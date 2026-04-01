using Sirenix.OdinInspector;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Gameplay.Knight
{
    public class KnightView : MonoBehaviour
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        [SerializeField, Required] private SpriteRenderer _spriteRenderer;
        [SerializeField, Required] private Animator _animator;

        public void UpdateMovement(Vector2 moveDirection)
        {
            var isMoving = moveDirection.sqrMagnitude > 0.0001f;

            if (isMoving)
            {
                _spriteRenderer.flipX = moveDirection.x < 0;
            }

            _animator.SetBool(IsRunning, isMoving);
        }
    }
}
