using Minesweeper_RC.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper_RC
{
    public partial class MainForm : Form
    {
        private Game _game;

        public MainForm()
        {
            InitializeComponent();
        }

        private void BeginnerGameMenu_Click(object sender, EventArgs e)
        {
            _game = new Game(Game.SkillLevel.Beginner);
            RenderField();
        }

        private void IntermediateGameMenu_Click(object sender, EventArgs e)
        {
            _game = new Game(Game.SkillLevel.Intermediate);
            RenderField();
        }

        private void ExpertGameMenu_Click(object sender, EventArgs e)
        {
            _game = new Game(Game.SkillLevel.Expert);
            RenderField();
        }

        private void RenderField()
        {
            // reset
            for (int i = this.Controls.Count - 1; i >= 0; i--)
                if (this.Controls[i].Tag.ToString() == "cell")
                    this.Controls.RemoveAt(i);

            // render
            for (var y = 0; y < _game.Settings.Height; y++)
            {
                for (var x = 0; x < _game.Settings.Width; x++)
                {
                    var cell = new Button()
                    {
                        Size = new Size(10, 10),
                        Location = new Point(x * 10, y * 10),
                    };
                    cell.Click += (sender, e) => Cell_Click(sender, e, x, y);
                }
            }
            this.Size = new Size(_game.Settings.Width * 10, _game.Settings.Height * 10);
        }

        private void Cell_Click(object sender, EventArgs e, int x, int y)
        {
            // reveal and render cell
            var control = (Control)sender;
            var revealedCells = _game.Reveal(x, y);
            var value = new Label()
            {
                Size = control.Size,
                Location = control.Location,
                Text = revealedCells[0].IsMine ? "M" : revealedCells[0].Neighbours > 0 ? revealedCells[0].Neighbours.ToString() : ""
            };
            this.Controls.Remove(control);

            // game over if mine

        }
    }
}
 