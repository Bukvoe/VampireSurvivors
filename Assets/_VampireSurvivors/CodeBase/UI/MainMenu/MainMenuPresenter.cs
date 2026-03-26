using System;
using _VampireSurvivors.CodeBase.Common;
using _VampireSurvivors.CodeBase.Services.Network;
using _VampireSurvivors.CodeBase.Services.SceneLoad;
using Cysharp.Threading.Tasks;
using R3;
using UnityEditor;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.UI.MainMenu
{
    public class MainMenuPresenter : IDisposable
    {
        private readonly MainMenuView _view;
        private readonly CompositeDisposable _disposable = new();

        private readonly INetworkService _networkService;
        private readonly ISceneLoadService _sceneLoadService;

        public MainMenuPresenter(MainMenuView view, INetworkService networkService, ISceneLoadService sceneLoadService)
        {
            _view = view;
            _networkService = networkService;
            _sceneLoadService = sceneLoadService;

            _view.HostRequested.Subscribe(_ => OnHostRequestedAsync().Forget()).AddTo(_disposable);
            _view.JoinRequested.Subscribe(_ => OnJoinRequested()).AddTo(_disposable);
            _view.QuitRequested.Subscribe(_ => OnQuitRequested()).AddTo(_disposable);
        }

        private async UniTask OnHostRequestedAsync()
        {
            _view.SetInteractable(false);

            var result = await _networkService.CreateSessionAsync();

            if (!result.Success)
            {
                Debug.LogError(result.ErrorMessage);
                _view.SetInteractable(true);
                return;
            }

            await _sceneLoadService.LoadSceneAsync(SceneName.GAMEPLAY);
        }

        private void OnJoinRequested()
        {
            Debug.Log(nameof(OnJoinRequested));
        }

        private void OnQuitRequested()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE_WIN
            Application.Quit();
#endif
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
