namespace BlockFill
{
    partial class BlockFill
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UI_BTN_Start = new System.Windows.Forms.Button();
            this.UI_TB_Steps = new System.Windows.Forms.TextBox();
            this.UI_LBL_Steps = new System.Windows.Forms.Label();
            this.UI_RB_Unbiased = new System.Windows.Forms.RadioButton();
            this.UI_RB_Biased = new System.Windows.Forms.RadioButton();
            this.UI_GB_Path = new System.Windows.Forms.GroupBox();
            this.UI_GB_Path.SuspendLayout();
            this.SuspendLayout();
            // 
            // UI_BTN_Start
            // 
            this.UI_BTN_Start.Location = new System.Drawing.Point(81, 130);
            this.UI_BTN_Start.Name = "UI_BTN_Start";
            this.UI_BTN_Start.Size = new System.Drawing.Size(75, 23);
            this.UI_BTN_Start.TabIndex = 0;
            this.UI_BTN_Start.Text = "Start";
            this.UI_BTN_Start.UseVisualStyleBackColor = true;
            this.UI_BTN_Start.Click += new System.EventHandler(this.UI_BTN_Start_Click);
            // 
            // UI_TB_Steps
            // 
            this.UI_TB_Steps.Location = new System.Drawing.Point(81, 91);
            this.UI_TB_Steps.Name = "UI_TB_Steps";
            this.UI_TB_Steps.Size = new System.Drawing.Size(75, 20);
            this.UI_TB_Steps.TabIndex = 1;
            this.UI_TB_Steps.Text = "0";
            this.UI_TB_Steps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // UI_LBL_Steps
            // 
            this.UI_LBL_Steps.AutoSize = true;
            this.UI_LBL_Steps.Location = new System.Drawing.Point(38, 98);
            this.UI_LBL_Steps.Name = "UI_LBL_Steps";
            this.UI_LBL_Steps.Size = new System.Drawing.Size(37, 13);
            this.UI_LBL_Steps.TabIndex = 2;
            this.UI_LBL_Steps.Text = "Steps:";
            // 
            // UI_RB_Unbiased
            // 
            this.UI_RB_Unbiased.AutoSize = true;
            this.UI_RB_Unbiased.Checked = true;
            this.UI_RB_Unbiased.Location = new System.Drawing.Point(20, 19);
            this.UI_RB_Unbiased.Name = "UI_RB_Unbiased";
            this.UI_RB_Unbiased.Size = new System.Drawing.Size(110, 17);
            this.UI_RB_Unbiased.TabIndex = 3;
            this.UI_RB_Unbiased.TabStop = true;
            this.UI_RB_Unbiased.Text = "Random Direction";
            this.UI_RB_Unbiased.UseVisualStyleBackColor = true;
            // 
            // UI_RB_Biased
            // 
            this.UI_RB_Biased.AutoSize = true;
            this.UI_RB_Biased.Location = new System.Drawing.Point(20, 39);
            this.UI_RB_Biased.Name = "UI_RB_Biased";
            this.UI_RB_Biased.Size = new System.Drawing.Size(102, 17);
            this.UI_RB_Biased.TabIndex = 4;
            this.UI_RB_Biased.Text = "Biased Direction";
            this.UI_RB_Biased.UseVisualStyleBackColor = true;
            // 
            // UI_GB_Path
            // 
            this.UI_GB_Path.Controls.Add(this.UI_RB_Biased);
            this.UI_GB_Path.Controls.Add(this.UI_RB_Unbiased);
            this.UI_GB_Path.Location = new System.Drawing.Point(34, 12);
            this.UI_GB_Path.Name = "UI_GB_Path";
            this.UI_GB_Path.Size = new System.Drawing.Size(160, 73);
            this.UI_GB_Path.TabIndex = 6;
            this.UI_GB_Path.TabStop = false;
            this.UI_GB_Path.Text = "Movement";
            // 
            // BlockFill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 159);
            this.Controls.Add(this.UI_GB_Path);
            this.Controls.Add(this.UI_LBL_Steps);
            this.Controls.Add(this.UI_TB_Steps);
            this.Controls.Add(this.UI_BTN_Start);
            this.Name = "BlockFill";
            this.Text = "Block Fill";
            this.UI_GB_Path.ResumeLayout(false);
            this.UI_GB_Path.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UI_BTN_Start;
        private System.Windows.Forms.TextBox UI_TB_Steps;
        private System.Windows.Forms.Label UI_LBL_Steps;
        private System.Windows.Forms.RadioButton UI_RB_Unbiased;
        private System.Windows.Forms.RadioButton UI_RB_Biased;
        private System.Windows.Forms.GroupBox UI_GB_Path;
    }
}

