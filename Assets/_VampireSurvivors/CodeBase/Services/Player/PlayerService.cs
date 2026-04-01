using System;
using System.Collections.Generic;
using _VampireSurvivors.CodeBase.Factories;
using _VampireSurvivors.CodeBase.Gameplay.Knight;
using _VampireSurvivors.CodeBase.Services.Network;
using Cysharp.Threading.Tasks;
using Fusion;
using R3;
using Zenject;

namespace _VampireSurvivors.CodeBase.Services.Player
{
    public class PlayerService : IInitializable, ITickable, IDisposable
    {
        private readonly FusionCallbacks _fusionCallbacks;
        private readonly NetworkRunner _runner;
        private readonly KnightFactory _knightFactory;
        private readonly CompositeDisposable _disposables = new();
        private readonly HashSet<PlayerRef> _spawningPlayers = new();

        public Knight LocalPlayer { get; private set; }

        public PlayerService(
            NetworkRunnerProvider runnerProvider,
            FusionCallbacks callbacks,
            KnightFactory knightFactory)
        {
            _runner = runnerProvider.Runner;
            _fusionCallbacks = callbacks;
            _knightFactory = knightFactory;
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

        public void Tick()
        {
            if (LocalPlayer != null)
            {
                return;
            }

            if (_runner.TryGetPlayerObject(_runner.LocalPlayer, out var networkObject)
                && networkObject.TryGetComponent<Knight>(out var knight))
            {
                LocalPlayer = knight;
            }
        }

        private void OnPlayerJoined(PlayerRef player)
        {
            SpawnPlayer(player).Forget();
        }

        private void OnPlayerLeft(PlayerRef player)
        {
            if (_runner.TryGetPlayerObject(player, out var networkObject))
            {
                _runner.Despawn(networkObject);
            }
        }

        private async UniTask SpawnPlayer(PlayerRef player)
        {
            if (!_runner.IsServer)
            {
                return;
            }

            if (!_runner.TryGetPlayerObject(player, out _) && _spawningPlayers.Add(player))
            {
                try
                {
                    var knight = await _knightFactory.CreateAsync();
                    var networkObject = knight.GetComponent<NetworkObject>();
                    networkObject.AssignInputAuthority(player);
                    _runner.SetPlayerObject(player, networkObject);
                }
                finally
                {
                    _spawningPlayers.Remove(player);
                }
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
