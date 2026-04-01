using Fusion;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Gameplay.Knight
{
    public class Knight : NetworkBehaviour
    {
        [SerializeField, Required] private KnightMovement _movement;
        [SerializeField, Required] private KnightView _view;

        private readonly CompositeDisposable _disposable = new();

        public override void Spawned()
        {
            _movement.MoveDirection.Subscribe(_view.UpdateMovement).AddTo(_disposable);
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _disposable.Dispose();
        }
    }
}
