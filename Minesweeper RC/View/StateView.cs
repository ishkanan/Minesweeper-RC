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

namespace Minesweeper_RC.View
{
    public partial class StateView : UserControl
    {
        public StateView()
        {
            InitializeComponent();
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
        SunState Sun { get; set; }
    }
}
