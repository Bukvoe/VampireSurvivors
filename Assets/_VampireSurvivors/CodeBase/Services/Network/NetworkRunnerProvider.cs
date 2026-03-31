using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Services.Network
{
    public class NetworkRunnerProvider
    {
        private readonly NetworkRunner _runnerPrefab;
        private readonly FusionCallbacks _fusionCallbacks;

        public NetworkRunner Runner { get; private set; }

        public NetworkRunnerProvider(NetworkRunner runnerPrefab, FusionCallbacks fusionCallbacks)
        {
            _runnerPrefab = runnerPrefab;
            _fusionCallbacks = fusionCallbacks;
        }

        public NetworkRunner CreateRunner()
        {
            if (Runner != null)
            {
                Debug.LogWarning("Runner already exists");
                return Runner;
            }

            Runner = Object.Instantiate(_runnerPrefab);
            Object.DontDestroyOnLoad(Runner);

            Runner.AddCallbacks(_fusionCallbacks);

            return Runner;
        }

        public async UniTask ResetRunnerAsync()
        {
            if (Runner == null)
            {
                return;
            }

            if (Runner.IsRunning)
            {
                await Runner.Shutdown();
            }

            Runner.RemoveCallbacks(_fusionCallbacks);

            Object.Destroy(Runner.gameObject);
            Runner = null;
        }
    }
}
