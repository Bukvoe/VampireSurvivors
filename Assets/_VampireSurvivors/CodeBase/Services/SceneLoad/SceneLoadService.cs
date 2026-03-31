using System.Linq;
using _VampireSurvivors.CodeBase.Common;
using _VampireSurvivors.CodeBase.Services.Network;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _VampireSurvivors.CodeBase.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        private readonly NetworkRunnerProvider _runnerProvider;

        public SceneLoadService(NetworkRunnerProvider runnerProvider)
        {
            _runnerProvider = runnerProvider;
        }

        public async UniTask LoadSceneAsync(string sceneName)
        {
            if (IsNetworkScene(sceneName))
            {
                var runner = _runnerProvider.Runner;

                if (runner.IsSceneAuthority)
                {
                    await runner.LoadScene(sceneName).ToUniTask();
                }

                return;
            }

            await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
        }

        private bool IsNetworkScene(string sceneName)
        {
            var networkSceneNames = new[]
            {
                SceneName.GAMEPLAY,
            };

            return networkSceneNames.Contains(sceneName);
        }
    }
}
