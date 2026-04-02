using Fusion;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Gameplay.Hero
{
    public class Hero : NetworkBehaviour
    {
        private readonly CompositeDisposable _disposable = new();

        [SerializeField, Required] private HeroMovement _movement;
        [SerializeField, Required] private HeroView _view;
        [SerializeField, Required] private HeroStatsNetwork _statsNetwork;

        [field: SerializeField, Required] public Transform CameraTarget { get; private set; }

        public HeroStats Stats { get; } = new();

        public override void Spawned()
        {
            _statsNetwork.BindStats(Stats);

            Stats.MoveSpeed.Subscribe(_movement.UpdateMaxSpeed).AddTo(_disposable);

            _movement.MoveDirection.Subscribe(_view.UpdateMovement).AddTo(_disposable);
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _disposable.Dispose();
        }
    }
}
