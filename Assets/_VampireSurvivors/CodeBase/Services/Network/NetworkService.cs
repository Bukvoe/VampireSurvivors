using _VampireSurvivors.CodeBase.Common.Extensions;
using _VampireSurvivors.CodeBase.Services.Network.Dto;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Services.Network
{
    public class NetworkService : INetworkService
    {
        private readonly NetworkRunnerProvider _runnerProvider;

        public NetworkService(NetworkRunnerProvider runnerProvider)
        {
            _runnerProvider = runnerProvider;
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

            return _runnerProvider.GetOrCreateRunner();
        }
    }
}
