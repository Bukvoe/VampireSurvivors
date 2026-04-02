using System;
using _VampireSurvivors.CodeBase.Common;
using _VampireSurvivors.CodeBase.Services.Network;
using _VampireSurvivors.CodeBase.Services.SceneLoad;
using Cysharp.Threading.Tasks;
using R3;
using Zenject;

namespace _VampireSurvivors.CodeBase.Ineteractors
{
    public class HandleDisconnectInteractor : IInitializable, IDisposable
    {
        private readonly INetworkService _networkService;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly CompositeDisposable _disposable = new();

        public HandleDisconnectInteractor(INetworkService networkService, ISceneLoadService sceneLoadService)
        {
            _networkService = networkService;
            _sceneLoadService = sceneLoadService;
        }

        public void Initialize()
        {
            _networkService.Disconnected.Subscribe(_ => LoadMainMenu()).AddTo(_disposable);
        }

        private void LoadMainMenu()
        {
            _sceneLoadService.LoadSceneAsync(SceneName.MENU).Forget();
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
