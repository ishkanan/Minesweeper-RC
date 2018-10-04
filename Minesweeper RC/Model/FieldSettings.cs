using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Minesweeper_RC.Model
{
    public class FieldSettings
    {
        public int Width { get; private set; }

        public int Height { get; private set; }

        public int MineCount { get; private set; }

        public FieldSettings(int width, int height, int mineCount)
        {
            Width = width;
            Height = height;
            MineCount = mineCount;
        }
    }
}
