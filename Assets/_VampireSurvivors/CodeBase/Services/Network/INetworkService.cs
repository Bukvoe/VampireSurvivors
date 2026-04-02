using _VampireSurvivors.CodeBase.Dto.Network;
using Cysharp.Threading.Tasks;
using R3;

namespace _VampireSurvivors.CodeBase.Services.Network
{
    public interface INetworkService
    {
        public Observable<Unit> Disconnected { get; }

        public UniTask<ConnectResult> CreateSessionAsync();

        public UniTask<ConnectResult> ConnectAsync(string sessionName);
    }
}
