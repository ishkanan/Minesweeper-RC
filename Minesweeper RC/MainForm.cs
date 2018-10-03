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
        }

        private void IntermediateGameMenu_Click(object sender, EventArgs e)
        {
            _game = new Game(Game.SkillLevel.Intermediate);
        }

        private void ExpertGameMenu_Click(object sender, EventArgs e)
        {
            _game = new Game(Game.SkillLevel.Expert);
        }
    }
}
 