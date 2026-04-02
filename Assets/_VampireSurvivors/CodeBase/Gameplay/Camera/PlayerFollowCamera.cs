using _VampireSurvivors.CodeBase.Services.Player;
using R3;
using UnityEngine;
using Zenject;

namespace _VampireSurvivors.CodeBase.Gameplay.Camera
{
    public class PlayerFollowCamera : MonoBehaviour
    {
        private readonly CompositeDisposable _disposables = new();

        private PlayerService _playerService;
        private Transform _target;

        [Inject]
        private void Construct(PlayerService playerService)
        {
            _playerService = playerService;
        }

        private void Start()
        {
            _playerService.LocalPlayer
                .Where(hero => hero != null)
                .Subscribe(hero =>
                {
                    _target = hero.CameraTarget;
                })
                .AddTo(_disposables);
        }

        private void LateUpdate()
        {
            if (_target == null)
            {
                return;
            }

            transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}
