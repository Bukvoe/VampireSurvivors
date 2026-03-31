using System;
using System.Collections.Generic;
using _VampireSurvivors.CodeBase.Services.Network;
using Cysharp.Threading.Tasks;
using Fusion;
using R3;
using Zenject;

namespace _VampireSurvivors.CodeBase.Services.Player
{
    public class PlayerService : IInitializable, IDisposable
    {
        private readonly FusionCallbacks _fusionCallbacks;
        private readonly NetworkRunner _runner;
        private readonly CompositeDisposable _disposables = new();

        public PlayerService(NetworkRunnerProvider runnerProvider, FusionCallbacks callbacks)
        {
            _runner = runnerProvider.Runner;
            _fusionCallbacks = callbacks;
        }

        public void Initialize()
        {
            _fusionCallbacks.PlayerJoined
                            .Subscribe(OnPlayerJoined)
                            .AddTo(_disposables);

            _fusionCallbacks.PlayerLeft
                            .Subscribe(OnPlayerLeft)
                            .AddTo(_disposables);

            if (!_runner.IsServer)
            {
                return;
            }

            foreach (var player in _runner.ActivePlayers)
            {
                SpawnPlayer(player).Forget();
            }
        }

        private void OnPlayerJoined(PlayerRef player)
        {
            SpawnPlayer(player).Forget();
        }

        private void OnPlayerLeft(PlayerRef player)
        {
        }

        private async UniTask SpawnPlayer(PlayerRef player)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
