using _VampireSurvivors.CodeBase.Services.Player;
using UnityEngine;
using Zenject;

namespace _VampireSurvivors.CodeBase.Gameplay.Camera
{
    public class PlayerFollowCamera : MonoBehaviour
    {
        private PlayerService _playerService;

        [Inject]
        private void Construct(PlayerService playerService)
        {
            _playerService = playerService;
        }

        private void LateUpdate()
        {
            var localPlayer = _playerService.LocalPlayer;

            if (localPlayer == null)
            {
                return;
            }

            transform.position = new Vector3(
                localPlayer.CameraTarget.position.x,
                localPlayer.CameraTarget.position.y,
                transform.position.z);
        }
    }
}
