using System;
using R3;
using UnityEditor;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.UI.MainMenu
{
    public class MainMenuPresenter : IDisposable
    {
        private readonly MainMenuView _view;
        private readonly CompositeDisposable _disposable = new();

        public MainMenuPresenter(MainMenuView view)
        {
            _view = view;

            _view.HostRequested.Subscribe(_ => OnHostRequested()).AddTo(_disposable);
            _view.JoinRequested.Subscribe(_ => OnJoinRequested()).AddTo(_disposable);
            _view.QuitRequested.Subscribe(_ => OnQuitRequested()).AddTo(_disposable);
        }

        private void OnHostRequested()
        {
            Debug.Log(nameof(OnHostRequested));
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
