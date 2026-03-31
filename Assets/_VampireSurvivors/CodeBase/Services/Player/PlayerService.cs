using System;
using System.Collections.Generic;
using _VampireSurvivors.CodeBase.Common;
using _VampireSurvivors.CodeBase.Factories;
using _VampireSurvivors.CodeBase.Services.Network;
using _VampireSurvivors.CodeBase.Services.SceneLoad;
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
        private readonly KnightFactory _knightFactory;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly CompositeDisposable _disposables = new();
        private readonly HashSet<PlayerRef> _spawningPlayers = new();

        public PlayerService(
            NetworkRunnerProvider runnerProvider,
            FusionCallbacks callbacks,
            KnightFactory knightFactory,
            ISceneLoadService sceneLoadService)
        {
            _runner = runnerProvider.Runner;
            _fusionCallbacks = callbacks;
            _knightFactory = knightFactory;
            _sceneLoadService = sceneLoadService;
        }

        public void Initialize()
        {
            _fusionCallbacks.PlayerJoined
                .Subscribe(OnPlayerJoined)
                .AddTo(_disposables);

            _fusionCallbacks.PlayerLeft
                .Subscribe(OnPlayerLeft)
                .AddTo(_disposables);

            _fusionCallbacks.Disconnected
                .Subscribe(_ => LoadMainMenu())
                .AddTo(_disposables);

            _fusionCallbacks.Shutdown
                .Subscribe(_ => LoadMainMenu())
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
            if (_runner.TryGetPlayerObject(player, out var networkObject))
            {
                _runner.Despawn(networkObject);
            }
        }

        private void LoadMainMenu()
        {
            _sceneLoadService.LoadSceneAsync(SceneName.MENU).Forget();
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
                    _runner.SetPlayerObject(player, knight.GetComponent<NetworkObject>());
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
