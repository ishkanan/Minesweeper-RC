using Minesweeper_RC.Extensions;
using Minesweeper_RC.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper_RC.View
{
    public partial class FieldView : UserControl, IFieldView
    {
        private Size _cellSize;

        public Size CellSize
        {
            get => _cellSize;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("size");
                if (value.Width < 1 || value.Height < 1)
                    throw new ArgumentOutOfRangeException("width and height must be greater than 0");
                _cellSize = value;
            }
        }

        public FieldView()
        {
            InitializeComponent();
            CellSize = new Size(10, 10);
        }

        public event CellClickEventHandler CellClick;

        public void RenderField(IField field)
        {
            Controls.Clear();
            for (var y = 0; y < field.FieldSize.Height; y++)
            {
                for (var x = 0; x < field.FieldSize.Width; x++)
                {
                    var cell = field.Get(x, y);
                    var btn = new Button()
                    {
                        Size = _cellSize,
                        Location = new Point(x * _cellSize.Width, y * _cellSize.Height),
                    };
                    btn.MouseUp += (sender, e) => Cell_MouseUp(sender, e, cell);
                    Controls.Add(btn);
                }
            }
            Size = field.FieldSize.MultiplyBy(_cellSize.Width, _cellSize.Height);
        }

        public bool AllowInput
        {
            get => this.Enabled;
            set
            {
                this.Enabled = value;
            }
        }

        private void Cell_MouseUp(object sender, MouseEventArgs e, Cell cell)
        {
            CellClick?.Invoke(cell, e.Button);
        }
    }

    public interface IFieldView
    {
        /// <summary>
        /// Set size of cell representation, in pixels.
        /// </summary>
        /// <param name="size"></param>
        Size CellSize { get; set; }
        /// <summary>
        /// Raised when a cell is clicked.
        /// </summary>
        event CellClickEventHandler CellClick;
        /// <summary>
        /// Renders a minefield.
        /// </summary>
        /// <param name="field">The field to render.</param>
        void RenderField(IField field);
        /// <summary>
        /// Determines if the user can click cells.
        /// </summary>
        bool AllowInput { get; set; }
    }

    /// <summary>
    /// Signature of the IFieldView.CellClick event.
    /// </summary>
    /// <param name="cell">The cell that was clicked.</param>
    /// <param name="buttons">The button(s) that was/were pressed.</param>
    public delegate void CellClickEventHandler(Cell cell, MouseButtons buttons);
}
