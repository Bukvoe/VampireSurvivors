using System.Linq;
using _VampireSurvivors.CodeBase.Common;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine.SceneManagement;

namespace _VampireSurvivors.CodeBase.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        private readonly NetworkRunner _networkRunner;

        public SceneLoadService(NetworkRunner networkRunner)
        {
            _networkRunner = networkRunner;
        }

        public async UniTask LoadSceneAsync(string sceneName)
        {
            if (IsNetworkScene(sceneName))
            {
                await _networkRunner.LoadScene(sceneName).ToUniTask();
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
