namespace MonopolySandbox
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.cmdDoStuff = new System.Windows.Forms.Button();
            this.cmdGetStats = new System.Windows.Forms.Button();
            this.cmdBuildHouse = new System.Windows.Forms.Button();
            this.cmdEndTurn = new System.Windows.Forms.Button();
            this.cmdBuyProperty = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(12, 12);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStatus.Size = new System.Drawing.Size(576, 397);
            this.txtStatus.TabIndex = 0;
            // 
            // cmdDoStuff
            // 
            this.cmdDoStuff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDoStuff.Location = new System.Drawing.Point(12, 415);
            this.cmdDoStuff.Name = "cmdDoStuff";
            this.cmdDoStuff.Size = new System.Drawing.Size(80, 23);
            this.cmdDoStuff.TabIndex = 1;
            this.cmdDoStuff.Text = "Next Turn";
            this.cmdDoStuff.UseVisualStyleBackColor = true;
            this.cmdDoStuff.Click += new System.EventHandler(this.cmdDoStuff_Click);
            // 
            // cmdGetStats
            // 
            this.cmdGetStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdGetStats.Location = new System.Drawing.Point(432, 415);
            this.cmdGetStats.Name = "cmdGetStats";
            this.cmdGetStats.Size = new System.Drawing.Size(75, 23);
            this.cmdGetStats.TabIndex = 2;
            this.cmdGetStats.Text = "Get Stats";
            this.cmdGetStats.UseVisualStyleBackColor = true;
            this.cmdGetStats.Click += new System.EventHandler(this.cmdGetStats_Click);
            // 
            // cmdBuildHouse
            // 
            this.cmdBuildHouse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdBuildHouse.Location = new System.Drawing.Point(193, 415);
            this.cmdBuildHouse.Name = "cmdBuildHouse";
            this.cmdBuildHouse.Size = new System.Drawing.Size(82, 23);
            this.cmdBuildHouse.TabIndex = 3;
            this.cmdBuildHouse.Text = "Build House";
            this.cmdBuildHouse.UseVisualStyleBackColor = true;
            this.cmdBuildHouse.Click += new System.EventHandler(this.cmdBuildHouse_Click);
            // 
            // cmdEndTurn
            // 
            this.cmdEndTurn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdEndTurn.Location = new System.Drawing.Point(513, 415);
            this.cmdEndTurn.Name = "cmdEndTurn";
            this.cmdEndTurn.Size = new System.Drawing.Size(75, 23);
            this.cmdEndTurn.TabIndex = 4;
            this.cmdEndTurn.Text = "End Turn";
            this.cmdEndTurn.UseVisualStyleBackColor = true;
            this.cmdEndTurn.Click += new System.EventHandler(this.cmdEndTurn_Click);
            // 
            // cmdBuyProperty
            // 
            this.cmdBuyProperty.Location = new System.Drawing.Point(98, 415);
            this.cmdBuyProperty.Name = "cmdBuyProperty";
            this.cmdBuyProperty.Size = new System.Drawing.Size(89, 23);
            this.cmdBuyProperty.TabIndex = 5;
            this.cmdBuyProperty.Text = "Buy Property";
            this.cmdBuyProperty.UseVisualStyleBackColor = true;
            this.cmdBuyProperty.Click += new System.EventHandler(this.cmdBuyProperty_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.cmdBuyProperty);
            this.Controls.Add(this.cmdEndTurn);
            this.Controls.Add(this.cmdBuildHouse);
            this.Controls.Add(this.cmdGetStats);
            this.Controls.Add(this.cmdDoStuff);
            this.Controls.Add(this.txtStatus);
            this.Name = "Form1";
            this.Text = "Monopoly Sandbox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtStatus;
        private Button cmdDoStuff;
        private Button cmdGetStats;
        private Button cmdBuildHouse;
        private Button cmdEndTurn;
        private Button cmdBuyProperty;
    }
}