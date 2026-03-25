using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _VampireSurvivors.CodeBase.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        public async UniTask LoadSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
        }
    }
}
