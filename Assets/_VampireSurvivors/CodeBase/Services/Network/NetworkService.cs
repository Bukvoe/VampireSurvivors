using System;
using _VampireSurvivors.CodeBase.Services.Network.Dto;
using Cysharp.Threading.Tasks;
using Fusion;

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

            if (result.Ok)
            {
                return new ConnectResult(result.Ok, string.Empty);
            }

            var errorMessage = $"{result.ShutdownReason}{Environment.NewLine}{result.ErrorMessage}";

            return new ConnectResult(false, errorMessage);
        }

            return new ConnectResult(false, errorMessage);
        }
    }
}
