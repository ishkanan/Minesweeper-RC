using Easy.MessageHub;
using Minesweeper_RC.Event;
using Minesweeper_RC.Model;
using Minesweeper_RC.View;
using System;
using System.Windows.Forms;

namespace Minesweeper_RC.Presenter
{
    public class FieldPresenter
    {
        private IFieldView _fieldView;
        private IGame _game;
        private IMessageHub _aggregator;

        public FieldPresenter(IFieldView fieldView, IGame game, IMessageHub aggregator)
        {
            _game = game;

            _fieldView = fieldView;
            _fieldView.CellClick += OnCellClick;
            _fieldView.RenderField(_game.Minefield);

            _aggregator = aggregator;
            _aggregator.Subscribe<GameStartedMessage>(m => OnGameStarted(m.Game));
        }

        private void OnGameStarted(IGame game)
        {
            _fieldView.RenderField(game.Minefield);
        }

        private void OnCellClick(Cell cell, MouseButtons buttons)
        {
            var isLeft = ((buttons & MouseButtons.Left) == MouseButtons.Left);
            var isRight = ((buttons & MouseButtons.Right) == MouseButtons.Right);

            if (isRight && !isLeft)
            {
                // flag or unflag a cell
                cell.IsFlagged = !cell.IsFlagged;
                _aggregator.Publish<FlaggedCountChangedMessage>(new FlaggedCountChangedMessage(_game));
            }
            else if (isLeft && !isRight)
            {
                // reveal a cell
                try
                {
                    _game.Reveal(cell.Location);
                }
                catch (InvalidOperationException)
                {
                    // ignore these errors as no impact to user
                    return;
                }

                // TODO: implement sun shocked on mouse down
                //_aggregator.Publish<>

                if (_game.State == GameState.Stopped)
                    _aggregator.Publish<GameFinishedMessage>(new GameFinishedMessage(_game));
            }
            else if (isLeft && isRight)
            {
                // TODO: implement game.AggressiveReveal
                throw new NotImplementedException();
            }

            // no more input is accepted if game is no longer running
            _fieldView.AllowInput = _game.State == GameState.Running;
            _fieldView.RenderField(_game.Minefield);
        }
    }
}
