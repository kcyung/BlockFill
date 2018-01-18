using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;
using System.Threading;

namespace BlockFill
{
    public partial class BlockFill : Form
    {
        // Delegate declaration
        private delegate void delVoidInt(int i);

        // Constants
        private const int WIDTH = 800;
        private const int HEIGHT = 600;
        private const int BLOCKSIZE = 10;
        private const int X_MAX = WIDTH / BLOCKSIZE;
        private const int Y_MAX = HEIGHT / BLOCKSIZE;
        private const int BASE_RGB_VALUE = 50;
        
        public static Random _rnd = new Random();       // Random number generator
        private Thread _thread = null;                  // Active thread checker
        

        public BlockFill()
        {
            InitializeComponent();
        }

        private void UI_BTN_Start_Click(object sender, EventArgs e)
        {
            // Program is still running
            if (_thread != null)
                return;
            if (UI_RB_Unbiased.Checked)
            {
                // Start a new thread to fill the block
                _thread = new Thread(TraverseGridRandomly);
                _thread.IsBackground = true;
                _thread.Start();
            }
        }

        // Thread to traverse 2D grid
        private void TraverseGridRandomly()
        {
            int steps = 0;
            CDrawer canvas = new CDrawer(WIDTH, HEIGHT);
            int[,] grid = new int[X_MAX, Y_MAX];    // 2D backing array for the grid  
            Point current = new Point(0, 0);
            Point previous = new Point(0, 0);

            List<Point> nextPossiblePoint = new List<Point>();

            canvas.Scale = BLOCKSIZE;
            // Initialize each grid location with nominal value;
            for (int x = 0; x < X_MAX; ++x)
                for (int y = 0; y < Y_MAX; ++y)
                    grid[x, y] = BASE_RGB_VALUE;

            while (!HasTravelledAll(grid))
            {
                ++steps;

                // Increment intensity of current color to max of 255
                grid[current.X, current.Y] = 
                    grid[current.X, current.Y] + 3 > 255 ? 255 : grid[current.X, current.Y] + 3;

                if (steps != 1)
                    canvas.SetBBScaledPixel(previous.X, previous.Y,
                        Color.FromArgb(grid[previous.X, previous.Y], grid[previous.X, previous.Y], 0));

                canvas.SetBBScaledPixel(current.X, current.Y, Color.Red);

                previous = current;

                nextPossiblePoint.Clear();
                nextPossiblePoint = validLocation(current);

                current = nextPossiblePoint[_rnd.Next(0, nextPossiblePoint.Count)];
            }

            canvas.AddText(steps.ToString() + " Steps Taken", 25, Color.Blue);
            try
            {
                Invoke(new delVoidInt(UpdateUI), steps);
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("Target thread is dead");
            }
            _thread = null;
        }

        private List<Point> validLocation(Point location)
        {
            List<Point> validPoints = new List<Point>();
            
            // Add points left, right, up, and down
            validPoints.Add(new Point(location.X - 1, location.Y));
            validPoints.Add(new Point(location.X + 1, location.Y));
            validPoints.Add(new Point(location.X, location.Y - 1));
            validPoints.Add(new Point(location.X, location.Y + 1));

            // remove all points out of bounds
            validPoints.RemoveAll(IsOutOfBounds);

            return validPoints;
        }

        // Check if the point is still with in the grid;
        private bool IsOutOfBounds(Point location)
        {
            if (location.X < 0 || location.X >= X_MAX || location.Y < 0 || location.Y >= Y_MAX)
                return true;
            return false;
        }

        // Checks if all cells have been travelled to
        private bool HasTravelledAll(int[,] array2D)
        {
            foreach (int i in array2D)
                if (i == BASE_RGB_VALUE)
                    return false;
            
            return true;
        }

        // Callback function for delegate
        private void UpdateUI(int count)
        {
            UI_TB_Steps.Text = count.ToString();
        }
    }
}
