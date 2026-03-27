using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Services.Network
{
    public class NetworkRunnerProvider
    {
        private readonly NetworkRunner _runnerPrefab;
        private NetworkRunner _runner;

        public NetworkRunnerProvider(NetworkRunner runnerPrefab)
        {
            _runnerPrefab = runnerPrefab;
        }

        public NetworkRunner GetOrCreateRunner()
        {
            if (_runner == null)
            {
                _runner = Object.Instantiate(_runnerPrefab);
                Object.DontDestroyOnLoad(_runner);
            }

            return _runner;
        }

        public async UniTask ResetRunnerAsync()
        {
            if (_runner == null)
            {
                return;
            }

            if (_runner.IsRunning)
            {
                await _runner.Shutdown();
            }

            Object.Destroy(_runner.gameObject);
            _runner = null;
        }
    }
}
