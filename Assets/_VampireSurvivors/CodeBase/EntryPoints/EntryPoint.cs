using _VampireSurvivors.CodeBase.Services.SceneLoad;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _VampireSurvivors.CodeBase.EntryPoints
{
    public class EntryPoint : IInitializable
    {
        private readonly ISceneLoadService _sceneLoadService;

        public EntryPoint(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
        }

        public void Initialize()
        {
            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            await _sceneLoadService.LoadSceneAsync("MenuScene");
        }
    }
}
