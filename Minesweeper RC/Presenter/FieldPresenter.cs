using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper_RC.Model;
using Minesweeper_RC.View;

namespace Minesweeper_RC.Presenter
{
    public class FieldPresenter
    {
        private IFieldView _fieldView;
        private IField _field;

        public FieldPresenter(IFieldView fieldView, IField field)
        {
            _fieldView = fieldView;
            _field = field;

            _fieldView.CellClick += OnCellClick;
            _fieldView.RenderField(_field);
        }

        private void OnCellClick(Cell cell, MouseButtons buttons)
        {
            // reveal cell via IGame
            _fieldView.RenderField(_field);
        }
    }
}
