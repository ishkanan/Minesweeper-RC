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
            this.SunImage = new System.Windows.Forms.PictureBox();
            this.MinesLeftLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SunImage)).BeginInit();
            this.SuspendLayout();
            // 
            // SunImage
            // 
            this.SunImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SunImage.Location = new System.Drawing.Point(129, 3);
            this.SunImage.Name = "SunImage";
            this.SunImage.Size = new System.Drawing.Size(30, 30);
            this.SunImage.TabIndex = 0;
            this.SunImage.TabStop = false;
            // 
            // MinesLeftLabel
            // 
            this.MinesLeftLabel.Location = new System.Drawing.Point(3, 3);
            this.MinesLeftLabel.Name = "MinesLeftLabel";
            this.MinesLeftLabel.Size = new System.Drawing.Size(120, 30);
            this.MinesLeftLabel.TabIndex = 1;
            this.MinesLeftLabel.Text = "000";
            // 
            // StateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MinesLeftLabel);
            this.Controls.Add(this.SunImage);
            this.Name = "StateView";
            this.Size = new System.Drawing.Size(289, 36);
            this.Load += new System.EventHandler(this.StateView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SunImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox SunImage;
        private System.Windows.Forms.Label MinesLeftLabel;
    }
}
