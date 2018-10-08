using Easy.MessageHub;
using Minesweeper_RC.Event;
using Minesweeper_RC.Model;
using Minesweeper_RC.View;
using System;
using System.Linq;

namespace Minesweeper_RC.Presenter
{
    public class StatePresenter
    {
        private IStateView _stateView;
        private IMessageHub _aggregator;

        public StatePresenter(IStateView stateView, IMessageHub aggregator)
        {
            _stateView = stateView;
            _stateView.SunClicked += OnSunClicked;

            _aggregator = aggregator;
            _aggregator.Subscribe<GameFinishedMessage>(m => OnGameFinished(m.Game));
            _aggregator.Subscribe<FlaggedCountChangedMessage>(m => OnFlaggedCountChanged(m.Game));
        }

        private void OnGameFinished(IGame game)
        {
            _stateView.Sun = game.Result == GameResult.Success ? SunState.Cool : SunState.Dead;
        }

        private void OnFlaggedCountChanged(IGame game)
        {
            var flagged = game.Minefield.Count(c => c.IsFlagged);
            _stateView.MinesToFlag = Math.Max(0, game.Settings.MineCount - flagged);
        }

        private void OnSunClicked(object sender, EventArgs e)
        {
            //TODO: add NewGameRequestedMessage
        }
    }
}
