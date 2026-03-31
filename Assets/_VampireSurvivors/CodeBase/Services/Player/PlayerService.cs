using System;
using System.Collections.Generic;
using _VampireSurvivors.CodeBase.Gameplay.Knight;
using _VampireSurvivors.CodeBase.Services.Network;
using Cysharp.Threading.Tasks;
using Fusion;
using R3;
using UnityEngine;
using Zenject;

namespace _VampireSurvivors.CodeBase.Services.Player
{
    public class PlayerService : IInitializable, IDisposable
    {
        private readonly FusionCallbacks _fusionCallbacks;
        private readonly NetworkRunner _runner;
        private readonly Knight _knightPrefab;
        private readonly CompositeDisposable _disposables = new();
        private readonly HashSet<PlayerRef> _spawningPlayers = new();

        public PlayerService(NetworkRunnerProvider runnerProvider, FusionCallbacks callbacks, Knight knightPrefab)
        {
            _runner = runnerProvider.Runner;
            _fusionCallbacks = callbacks;
            _knightPrefab = knightPrefab;
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
            if (!_runner.IsServer)
            {
                return;
            }

            if (!_runner.TryGetPlayerObject(player, out _) && _spawningPlayers.Add(player))
            {
                try
                {
                    var playerObject = await _runner.SpawnAsync(_knightPrefab, Vector3.zero, Quaternion.identity, player);
                    _runner.SetPlayerObject(player, playerObject.GetComponent<NetworkObject>());
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
