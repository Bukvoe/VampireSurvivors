using _VampireSurvivors.CodeBase.Common.Extensions;
using _VampireSurvivors.CodeBase.Services.Network.Dto;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Services.Network
{
    public class NetworkService : INetworkService
    {
        private readonly NetworkRunner _networkRunner;

        public NetworkService(NetworkRunner networkRunner)
        {
            _networkRunner = networkRunner;
        }

        public async UniTask<ConnectResult> CreateSessionAsync()
        {
            var result = await _networkRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Host,
                IsOpen = true,
                IsVisible = true,
            });

            return result.Ok
                ? new ConnectResult(result.Ok, string.Empty)
                : new ConnectResult(false, result.ToErrorMessage());
        }

        public async UniTask<ConnectResult> ConnectAsync(string sessionName)
        {
            var result = await _networkRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Client,
                SessionName = sessionName,
            });

            return result.Ok
                ? new ConnectResult(true, string.Empty)
                : new ConnectResult(false, result.ToErrorMessage());
        }

    }
}
