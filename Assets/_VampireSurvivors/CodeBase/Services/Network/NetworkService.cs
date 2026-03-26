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

        public async UniTask<HostResult> HostAsync()
        {
            var startGameResult = await _networkRunner.StartGame(new StartGameArgs
            {
                GameMode = GameMode.Host,
                IsOpen = true,
                IsVisible = true,
            });

            if (startGameResult.Ok)
            {
                return new HostResult(startGameResult.Ok, string.Empty);
            }

            var errorMessage = $"{startGameResult.ShutdownReason}{Environment.NewLine}{startGameResult.ErrorMessage}";

            return new HostResult(false, errorMessage);
        }
    }
}
