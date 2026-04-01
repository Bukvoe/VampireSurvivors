using _VampireSurvivors.CodeBase.Dto.Input;
using Fusion;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Gameplay.Knight
{
    public class KnightMovement : NetworkBehaviour
    {
        public readonly ReactiveProperty<Vector2> MoveDirection = new();

        [SerializeField, Required] private float _maxSpeed;
        [SerializeField, Required] private Rigidbody2D _rigidbody;

        [Networked, OnChangedRender(nameof(OnMoveDirectionChanged))]
        private Vector2 NetworkMoveDirection { get; set; }

        public override void FixedUpdateNetwork()
        {
            if (!GetInput(out NetworkPlayerInput input))
            {
                return;
            }

            var moveNormalized = input.MoveDirection.normalized;

            var deltaPosition = moveNormalized * _maxSpeed * Runner.DeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + deltaPosition);

            NetworkMoveDirection = moveNormalized;
        }

        private void OnMoveDirectionChanged()
        {
            MoveDirection.Value = NetworkMoveDirection;
        }
    }
}
