namespace Minesweeper_RC.View
{
    partial class StateView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateView));
            this.MinesToFlagLabel = new System.Windows.Forms.Label();
            this.SunImageList = new System.Windows.Forms.ImageList(this.components);
            this.ElapsedTimeLabel = new System.Windows.Forms.Label();
            this.SunButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MinesToFlagLabel
            // 
            this.MinesToFlagLabel.BackColor = System.Drawing.Color.Black;
            this.MinesToFlagLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MinesToFlagLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinesToFlagLabel.ForeColor = System.Drawing.Color.Red;
            this.MinesToFlagLabel.Location = new System.Drawing.Point(22, 2);
            this.MinesToFlagLabel.Name = "MinesToFlagLabel";
            this.MinesToFlagLabel.Size = new System.Drawing.Size(82, 42);
            this.MinesToFlagLabel.TabIndex = 1;
            this.MinesToFlagLabel.Text = "000";
            this.MinesToFlagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SunImageList
            // 
            this.SunImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SunImageList.ImageStream")));
            this.SunImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.SunImageList.Images.SetKeyName(0, "Happy");
            this.SunImageList.Images.SetKeyName(1, "Shocked");
            this.SunImageList.Images.SetKeyName(2, "Cool");
            this.SunImageList.Images.SetKeyName(3, "Dead");
            // 
            // ElapsedTimeLabel
            // 
            this.ElapsedTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ElapsedTimeLabel.BackColor = System.Drawing.Color.Black;
            this.ElapsedTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ElapsedTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElapsedTimeLabel.ForeColor = System.Drawing.Color.Red;
            this.ElapsedTimeLabel.Location = new System.Drawing.Point(189, 2);
            this.ElapsedTimeLabel.Name = "ElapsedTimeLabel";
            this.ElapsedTimeLabel.Size = new System.Drawing.Size(82, 42);
            this.ElapsedTimeLabel.TabIndex = 1;
            this.ElapsedTimeLabel.Text = "000";
            this.ElapsedTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SunButton
            // 
            this.SunButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SunButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SunButton.Location = new System.Drawing.Point(124, 3);
            this.SunButton.Name = "SunButton";
            this.SunButton.Size = new System.Drawing.Size(41, 41);
            this.SunButton.TabIndex = 2;
            this.SunButton.UseVisualStyleBackColor = true;
            this.SunButton.Click += new System.EventHandler(this.SunButton_Click);
            // 
            // StateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SunButton);
            this.Controls.Add(this.ElapsedTimeLabel);
            this.Controls.Add(this.MinesToFlagLabel);
            this.Name = "StateView";
            this.Size = new System.Drawing.Size(289, 46);
            this.Load += new System.EventHandler(this.StateView_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label MinesToFlagLabel;
        private System.Windows.Forms.Label ElapsedTimeLabel;
        public System.Windows.Forms.Button SunButton;
        public System.Windows.Forms.ImageList SunImageList;
    }
}
