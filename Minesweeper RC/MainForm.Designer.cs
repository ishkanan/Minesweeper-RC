namespace Minesweeper_RC
{
    partial class MainForm
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
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.GameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BeginnerGameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.IntermediateGameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExpertGameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopMenuStrip
            // 
            this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameMenu});
            this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuStrip.Name = "TopMenuStrip";
            this.TopMenuStrip.Size = new System.Drawing.Size(678, 24);
            this.TopMenuStrip.TabIndex = 0;
            this.TopMenuStrip.Text = "menuStrip1";
            // 
            // GameMenu
            // 
            this.GameMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BeginnerGameMenu,
            this.IntermediateGameMenu,
            this.ExpertGameMenu});
            this.GameMenu.Name = "GameMenu";
            this.GameMenu.Size = new System.Drawing.Size(50, 20);
            this.GameMenu.Text = "Game";
            // 
            // BeginnerGameMenu
            // 
            this.BeginnerGameMenu.Name = "BeginnerGameMenu";
            this.BeginnerGameMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.BeginnerGameMenu.Size = new System.Drawing.Size(181, 22);
            this.BeginnerGameMenu.Text = "Beginner";
            this.BeginnerGameMenu.Click += new System.EventHandler(this.BeginnerGameMenu_Click);
            // 
            // IntermediateGameMenu
            // 
            this.IntermediateGameMenu.Name = "IntermediateGameMenu";
            this.IntermediateGameMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.IntermediateGameMenu.Size = new System.Drawing.Size(181, 22);
            this.IntermediateGameMenu.Text = "Intermediate";
            this.IntermediateGameMenu.Click += new System.EventHandler(this.IntermediateGameMenu_Click);
            // 
            // ExpertGameMenu
            // 
            this.ExpertGameMenu.Name = "ExpertGameMenu";
            this.ExpertGameMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.ExpertGameMenu.Size = new System.Drawing.Size(181, 22);
            this.ExpertGameMenu.Text = "Expert";
            this.ExpertGameMenu.Click += new System.EventHandler(this.ExpertGameMenu_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 466);
            this.Controls.Add(this.TopMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minesweeper RC";
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip TopMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem GameMenu;
        private System.Windows.Forms.ToolStripMenuItem BeginnerGameMenu;
        private System.Windows.Forms.ToolStripMenuItem IntermediateGameMenu;
        private System.Windows.Forms.ToolStripMenuItem ExpertGameMenu;
    }
}

