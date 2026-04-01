using System;
using _VampireSurvivors.CodeBase.Common.Extensions;
using _VampireSurvivors.CodeBase.Dto.Network;
using Cysharp.Threading.Tasks;
using Fusion;
using R3;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _VampireSurvivors.CodeBase.Services.Network
{
    public class NetworkService : INetworkService, IInitializable, IDisposable
    {
        private readonly NetworkRunnerProvider _runnerProvider;
        private readonly FusionCallbacks _fusionCallbacks;
        private readonly CompositeDisposable _disposable = new();
        private readonly Subject<Unit> _disconnected = new();

        public Observable<Unit> Disconnected => _disconnected;

        public NetworkService(NetworkRunnerProvider runnerProvider, FusionCallbacks fusionCallbacks)
        {
            _runnerProvider = runnerProvider;
            _fusionCallbacks = fusionCallbacks;
        }

        public void Initialize()
        {
            _fusionCallbacks.Disconnected.Subscribe(_ => _disconnected.OnNext(Unit.Default)).AddTo(_disposable);
            _fusionCallbacks.Shutdown.Subscribe(_ => _disconnected.OnNext(Unit.Default)).AddTo(_disposable);
        }

        public async UniTask<ConnectResult> CreateSessionAsync()
        {
            var runner = await CreateNewRunner();

            var session = Random.Range(0, 100).ToString();
            Debug.Log($"Creating session {session}");

            var result = await runner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Host,
                IsOpen = true,
                IsVisible = true,
                SessionName = session,
            });

            return result.Ok
                ? new ConnectResult(result.Ok, string.Empty)
                : new ConnectResult(false, result.ToErrorMessage());
        }

        public async UniTask<ConnectResult> ConnectAsync(string sessionName)
        {
            var runner = await CreateNewRunner();

            var result = await runner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Client,
                SessionName = sessionName,
            });

            return result.Ok
                ? new ConnectResult(true, string.Empty)
                : new ConnectResult(false, result.ToErrorMessage());
        }

        private async UniTask<NetworkRunner> CreateNewRunner()
        {
            await _runnerProvider.ResetRunnerAsync();

            return _runnerProvider.CreateRunner();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
