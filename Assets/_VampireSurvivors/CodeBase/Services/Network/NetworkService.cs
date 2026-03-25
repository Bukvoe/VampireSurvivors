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
    }
}
