using Sirenix.OdinInspector;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Gameplay.Hero
{
    public class HeroView : MonoBehaviour
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        [SerializeField, Required] private SpriteRenderer _spriteRenderer;
        [SerializeField, Required] private Animator _animator;

        public void UpdateMovement(Vector2 moveDirection)
        {
            const float ROTATION_THRESHOLD = 0.01f;

            if (Mathf.Abs(moveDirection.x) > ROTATION_THRESHOLD)
            {
                _spriteRenderer.flipX = moveDirection.x < 0;
            }

            var isMoving = moveDirection.sqrMagnitude > 0.0001f;
            _animator.SetBool(IsRunning, isMoving);
        }
    }
}
