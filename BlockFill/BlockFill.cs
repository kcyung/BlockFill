// Ka Chun Yung
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

        // Constants for window dimensions, 2D array, scaling, and base color intensity
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

            UI_BTN_Start.Enabled = false;
            UI_BTN_Start.Text = "Running";

            if (UI_RB_Unbiased.Checked)
            {
                // Start a new thread to fill the block
                _thread = new Thread(TraverseGridRandomly);
                _thread.IsBackground = true;
                _thread.Start();
            }
            else  // Biased grid traverse selected
            {
                _thread = new Thread(BiasedTraverse);
                _thread.IsBackground = true;
                _thread.Start();
            }
        }

        // Thread to traverse 2D grid in random fashion
        private void TraverseGridRandomly()
        {
            int steps = 0;
            CDrawer canvas = new CDrawer(WIDTH, HEIGHT);
            int[,] grid = new int[X_MAX, Y_MAX];    // 2D backing array for the grid  
            Point current = new Point(0, 0);
            Point previous = new Point(0, 0);

            List<Point> nextPossiblePoint = new List<Point>();      // List of valid moves

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

                // Draw previous point in yellow - ignore initial start
                if (steps != 1)
                    canvas.SetBBScaledPixel(previous.X, previous.Y,
                        Color.FromArgb(grid[previous.X, previous.Y], grid[previous.X, previous.Y], 0));

                // Draw current point location in red
                canvas.SetBBScaledPixel(current.X, current.Y, Color.Red);

                previous = current;

                // Clear previous list of valid movement and update list and move to new Point
                nextPossiblePoint.Clear();
                nextPossiblePoint = validLocation(current);
                current = nextPossiblePoint[_rnd.Next(0, nextPossiblePoint.Count)];
            }

            // Display number of steps on screen
            canvas.AddText(steps.ToString() + " Steps Taken", 25, Color.Blue);

            try
            {
                Invoke(new delVoidInt(UpdateUI), steps);
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("Target thread is dead");
            }
        }

        // Biased Grid Traverse Thread
        private void BiasedTraverse()
        {
            int steps = 0;
            CDrawer canvas = new CDrawer(WIDTH, HEIGHT);
            int[,] grid = new int[X_MAX, Y_MAX];    // 2D backing array of color intensity for the grid  
            Point current = new Point(0, 0);
            Point previous = new Point(0, 0);

            // List of valid biased movements
            List<Point> nextPossiblePoint = new List<Point>() { current };

            // List of all points not travelled to
            List<Point> HaveNotTraversed = new List<Point>();

            canvas.Scale = BLOCKSIZE;

            // Initialization: each grid location with base color intensity 
            //                 and include all points as not travelled to
            for (int x = 0; x < X_MAX; ++x)
                for (int y = 0; y < Y_MAX; ++y)
                {
                    grid[x, y] = BASE_RGB_VALUE;
                    HaveNotTraversed.Add(new Point(x, y));
                }

            while (HaveNotTraversed.Count > 0)
            {
                ++steps;

                HaveNotTraversed.Remove(current);

                // Increment intensity of current color to max of 255
                grid[current.X, current.Y] =
                    grid[current.X, current.Y] + 25 > 255 ? 255 : grid[current.X, current.Y] + 25;

                // Draw previous point in yellow
                if (steps != 1)
                    canvas.SetBBScaledPixel(previous.X, previous.Y,
                        Color.FromArgb(grid[previous.X, previous.Y], grid[previous.X, previous.Y], 0));

                // Draw current point in red
                canvas.SetBBScaledPixel(current.X, current.Y, Color.Red);

                previous = current;

                // Clear list and update biased travel directions and move
                nextPossiblePoint.Clear();
                nextPossiblePoint = ClosestSpace(current, HaveNotTraversed);

                if (nextPossiblePoint.Count > 0)
                    current = nextPossiblePoint[_rnd.Next(0, nextPossiblePoint.Count)];
            }

            // Display number of steps to complete travel
            canvas.AddText(steps.ToString() + " Steps Taken", 25, Color.Blue);

            try
            {
                Invoke(new delVoidInt(UpdateUI), steps);
            }
            catch
            {
                System.Diagnostics.Trace.WriteLine("Target thread is dead");
            }
        }

        // Support Methods 
        // Returns a list of best biased move
        private List<Point> ClosestSpace(Point current, List<Point> Empty)
        {
            List<Point> biasedPoints = new List<Point>();
            List<Point> closestPoints = new List<Point>();

            // Movement of one unit from current location
            Point left = new Point(current.X - 1, current.Y);
            Point right = new Point(current.X + 1, current.Y);
            Point up = new Point(current.X, current.Y - 1);
            Point down = new Point(current.X, current.Y + 1);

            // Finding the closest spot that has not been travelled to
            int closestDistance = X_MAX + Y_MAX;
            
            Empty.ForEach(o => closestDistance =
                Math.Min(closestDistance, Math.Abs(current.X - o.X) + Math.Abs(current.Y - o.Y)));

            // Create a list of the closest points that have not been travelled to
            Empty.ForEach(o => 
                { if (Math.Abs(current.X - o.X) + Math.Abs(current.Y - o.Y) == closestDistance)
                    closestPoints.Add(o);
                });

            // Add the direction to move to the closest point
            foreach(Point p in closestPoints)
            {
                if (p.X < current.X)
                    biasedPoints.Add(left);

                if (p.X > current.X)
                    biasedPoints.Add(right);

                if (p.Y < current.Y)
                    biasedPoints.Add(up);

                if (p.Y > current.Y)
                    biasedPoints.Add(down);
            }

            return biasedPoints;
        }

        // Returns a list of points for next possible move - all in bounds
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

        // Predicate to check if point is in the grid
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
            UI_BTN_Start.Text = "Start";
            UI_BTN_Start.Enabled = true;
            _thread = null;
        }
    }
}