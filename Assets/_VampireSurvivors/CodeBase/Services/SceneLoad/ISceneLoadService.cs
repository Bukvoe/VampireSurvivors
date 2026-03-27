using Cysharp.Threading.Tasks;

namespace _VampireSurvivors.CodeBase.Services.SceneLoad
{
    public interface ISceneLoadService
    {
        UniTask LoadSceneAsync(string sceneName);
    }
}
