using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper_RC.Utility;
using System.Drawing.Text;

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
            //_fonts.AddMemoryFont(Utility.Utility.GetFontResourcePointer)
        }

        public SunState Sun
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public int MinesToFlag
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public int ElapsedMilliseconds
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        private void StateView_Load(object sender, EventArgs e)
        {

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
        SunState Sun { get; set; }
        /// <summary>
        /// Number of mines left to flag.
        /// </summary>
        int MinesToFlag { get; set; }
        /// <summary>
        /// Total elapsed milliseconds. Resolution is implementation-specific.
        /// </summary>
        int ElapsedMilliseconds { get; set; }
    }
}
