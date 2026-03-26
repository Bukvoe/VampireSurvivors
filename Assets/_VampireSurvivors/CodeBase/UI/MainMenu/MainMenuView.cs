using System.ComponentModel.DataAnnotations;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _VampireSurvivors.CodeBase.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        public readonly Subject<Unit> HostRequested = new();
        public readonly Subject<Unit> JoinRequested = new();
        public readonly Subject<Unit> QuitRequested = new();

        [SerializeField, Required] private Button _hostButton;
        [SerializeField, Required] private TMP_InputField _roomInputField;
        [SerializeField, Required] private Button _joinButton;
        [SerializeField, Required] private Button _quitButton;

        private void Awake()
        {
            _hostButton.onClick.AddListener(() => HostRequested.OnNext(Unit.Default));
            _joinButton.onClick.AddListener(() => JoinRequested.OnNext(Unit.Default));
            _quitButton.onClick.AddListener(() => QuitRequested.OnNext(Unit.Default));
        }

        private void OnDestroy()
        {
            _hostButton.onClick.RemoveAllListeners();
        }

        public void SetInteractable(bool interactable)
        {
            _hostButton.interactable = interactable;
            _joinButton.interactable = interactable;
            _roomInputField.interactable = interactable;
        }
    }
}
