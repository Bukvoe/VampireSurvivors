using _VampireSurvivors.CodeBase.Services.Network.Dto;
using Cysharp.Threading.Tasks;

namespace _VampireSurvivors.CodeBase.Services.Network
{
    public interface INetworkService
    {
        public UniTask<HostResult> HostAsync();
    }
}
