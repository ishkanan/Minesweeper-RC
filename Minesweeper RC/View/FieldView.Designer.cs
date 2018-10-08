namespace Minesweeper_RC.View
{
    partial class FieldView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FieldView));
            this.CellImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // CellImageList
            // 
            this.CellImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("CellImageList.ImageStream")));
            this.CellImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.CellImageList.Images.SetKeyName(0, "Flag");
            this.CellImageList.Images.SetKeyName(1, "TrippedMine");
            // 
            // FieldView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "FieldView";
            this.Size = new System.Drawing.Size(326, 304);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList CellImageList;
    }
}
