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
        private IGame _game;

        public FieldPresenter(IFieldView fieldView, IGame game)
        {
            _fieldView = fieldView;
            _game = game;

            _fieldView.CellClick += OnCellClick;
            _fieldView.RenderField(_game.Minefield);
        }

        private void OnCellClick(Cell cell, MouseButtons buttons)
        {
            try
            {
                _game.Reveal(cell.Location);
            }
            catch (InvalidOperationException)
            {
                // ignore these errors as no impact to user
                return;
            }

            // no more input is accepted if game is no longer running
            _fieldView.AllowInput = _game.State == GameState.Running;
            _fieldView.RenderField(_game.Minefield);
        }
    }
}
