using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Resources;
using Minesweeper_RC.Utility;

namespace Minesweeper_RC.View
{
    public partial class StateView : UserControl, IStateView
    {
        private PrivateFontCollection _fonts;

        public StateView()
        {
            InitializeComponent();

            // load the custom 7-segment font
            _fonts = new PrivateFontCollection();
            var resManager = new ResourceManager(typeof(Minesweeper_RC.Properties.Resources));
            var font = Utility.Utility.GetResourceBytesPointer(resManager, "SevenSegmentFont");
            _fonts.AddMemoryFont(font.Item1, font.Item2);
        }

        public SunState Sun
        {
            set => SunButton.Image = SunImageList.Images[value.ToString()];
        }

        public int MinesToFlag
        {
            set => MinesToFlagLabel.Text = value.ToString();
        }

        public int ElapsedMilliseconds
        {
            set => ElapsedTimeLabel.Text = (value / 1000).ToString();
        }

        public event EventHandler SunClicked;

        private void StateView_Load(object sender, EventArgs e)
        {
            // assign the custom 7-segment font
            MinesToFlagLabel.Font = new Font(_fonts.Families[0], 25);
            ElapsedTimeLabel.Font = MinesToFlagLabel.Font;
        }

        private void SunButton_Click(object sender, EventArgs e)
        {
            SunClicked?.Invoke(this, e);
        }
    }

    public enum SunState
    {
        Happy = 0,
        Shocked,
        Cool,
        Dead
    }

    public interface IStateView
    {
        /// <summary>
        /// The state of that Sun guy.
        /// </summary>
        SunState Sun { set; }
        /// <summary>
        /// Number of mines left to flag.
        /// </summary>
        int MinesToFlag { set; }
        /// <summary>
        /// Total elapsed milliseconds. Resolution is implementation-specific.
        /// </summary>
        int ElapsedMilliseconds { set; }
        /// <summary>
        /// Event raised when Sun is clicked.
        /// </summary>
        event EventHandler SunClicked;
    }
}
