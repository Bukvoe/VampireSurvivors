using System;
using _VampireSurvivors.CodeBase.Gameplay.Hero;
using _VampireSurvivors.CodeBase.Services.Player;
using R3;
using Zenject;

namespace _VampireSurvivors.CodeBase.UI.Gameplay
{
    public class StatsPresenter : IInitializable, IDisposable
    {
        private readonly StatsView _statsView;
        private readonly PlayerService _playerService;

        private readonly CompositeDisposable _disposables = new();
        private CompositeDisposable _playerStatsDisposables = new();

        public StatsPresenter(StatsView statsView, PlayerService playerService)
        {
            _statsView = statsView;
            _playerService = playerService;
        }

        public void Initialize()
        {
            _playerService.LocalPlayer
                .Subscribe(OnPlayerChanged)
                .AddTo(_disposables);
        }

        private void OnPlayerChanged(Hero hero)
        {
            _playerStatsDisposables.Dispose();
            _playerStatsDisposables = new CompositeDisposable();

            if (hero == null)
            {
                return;
            }

            hero.Stats.MoveSpeed
                .Subscribe(x =>
                {
                    _statsView.UpdateMoveSpeed(x);
                })
                .AddTo(_playerStatsDisposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
