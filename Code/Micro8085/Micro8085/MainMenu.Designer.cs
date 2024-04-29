namespace Micro8085
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.Start = new System.Windows.Forms.Label();
            this.Help = new System.Windows.Forms.Label();
            this.About_us = new System.Windows.Forms.Label();
            this.Instruction_Set = new System.Windows.Forms.Label();
            this.Res_and_Ref = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.AutoSize = true;
            this.Start.BackColor = System.Drawing.Color.Transparent;
            this.Start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Start.Font = new System.Drawing.Font("Segoe Print", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Start.Location = new System.Drawing.Point(170, 63);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(101, 50);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Help
            // 
            this.Help.AutoSize = true;
            this.Help.BackColor = System.Drawing.Color.Transparent;
            this.Help.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Help.Font = new System.Drawing.Font("Segoe Print", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Help.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Help.Location = new System.Drawing.Point(120, 101);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(87, 50);
            this.Help.TabIndex = 1;
            this.Help.Text = "Help";
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // About_us
            // 
            this.About_us.AutoSize = true;
            this.About_us.BackColor = System.Drawing.Color.Transparent;
            this.About_us.Cursor = System.Windows.Forms.Cursors.Hand;
            this.About_us.Font = new System.Drawing.Font("Segoe Print", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.About_us.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.About_us.Location = new System.Drawing.Point(66, 201);
            this.About_us.Name = "About_us";
            this.About_us.Size = new System.Drawing.Size(156, 50);
            this.About_us.TabIndex = 2;
            this.About_us.Text = "About Us";
            this.About_us.Click += new System.EventHandler(this.About_us_Click);
            // 
            // Instruction_Set
            // 
            this.Instruction_Set.AutoSize = true;
            this.Instruction_Set.BackColor = System.Drawing.Color.Transparent;
            this.Instruction_Set.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Instruction_Set.Font = new System.Drawing.Font("Segoe Print", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Instruction_Set.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Instruction_Set.Location = new System.Drawing.Point(76, 151);
            this.Instruction_Set.Name = "Instruction_Set";
            this.Instruction_Set.Size = new System.Drawing.Size(234, 50);
            this.Instruction_Set.TabIndex = 3;
            this.Instruction_Set.Text = "Instruction set";
            this.Instruction_Set.Click += new System.EventHandler(this.Instruction_Set_Click);
            // 
            // Res_and_Ref
            // 
            this.Res_and_Ref.AutoSize = true;
            this.Res_and_Ref.BackColor = System.Drawing.Color.Transparent;
            this.Res_and_Ref.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Res_and_Ref.Font = new System.Drawing.Font("Segoe Print", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Res_and_Ref.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Res_and_Ref.Location = new System.Drawing.Point(76, 251);
            this.Res_and_Ref.Name = "Res_and_Ref";
            this.Res_and_Ref.Size = new System.Drawing.Size(381, 50);
            this.Res_and_Ref.TabIndex = 5;
            this.Res_and_Ref.Text = "Resource and References";
            this.Res_and_Ref.Click += new System.EventHandler(this.Res_and_Ref_Click);
            // 
            // Exit
            // 
            this.Exit.AutoSize = true;
            this.Exit.BackColor = System.Drawing.Color.Transparent;
            this.Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit.Font = new System.Drawing.Font("Segoe Print", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.Exit.Location = new System.Drawing.Point(106, 301);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(80, 50);
            this.Exit.TabIndex = 6;
            this.Exit.Text = "Exit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Beige;
            this.BackgroundImage = global::Micro8085.Properties.Resources.U11;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(835, 421);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Res_and_Ref);
            this.Controls.Add(this.Instruction_Set);
            this.Controls.Add(this.About_us);
            this.Controls.Add(this.Help);
            this.Controls.Add(this.Start);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Gray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Start;
        private System.Windows.Forms.Label Help;
        private System.Windows.Forms.Label About_us;
        private System.Windows.Forms.Label Instruction_Set;
        private System.Windows.Forms.Label Res_and_Ref;
        private System.Windows.Forms.Label Exit;
    }
}