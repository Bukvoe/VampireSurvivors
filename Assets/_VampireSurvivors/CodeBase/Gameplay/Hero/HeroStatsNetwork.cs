using _VampireSurvivors.CodeBase.Config;
using Fusion;
using R3;

namespace _VampireSurvivors.CodeBase.Gameplay.Hero
{
    public class HeroStatsNetwork : NetworkBehaviour, IAfterSpawned
    {
        private readonly CompositeDisposable _compositeDisposable = new();

        private HeroStats _stats;
        private ChangeDetector _changeDetector;

        [Networked] private float MoveSpeedNetwork { get; set; }

        public void BeforeSpawned(HeroStatsConfig statsConfig)
        {
            MoveSpeedNetwork = statsConfig.MoveSpeed;
        }

        public override void Spawned()
        {
            _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
        }

        public void BindStats(HeroStats stats)
        {
            _stats = stats;
        }

        public void AfterSpawned()
        {
            UpdateStats();

            _stats.MoveSpeed.Subscribe(x =>
            {
                if (Object.HasStateAuthority)
                {
                    MoveSpeedNetwork = x;
                }
            }).AddTo(_compositeDisposable);
        }

        public override void FixedUpdateNetwork()
        {
            foreach (var change in _changeDetector.DetectChanges(this))
            {
                switch (change)
                {
                    case nameof(MoveSpeedNetwork):
                        OnMoveSpeedNetworkChanged();
                        break;
                }
            }
        }

        public override void Despawned(NetworkRunner runner, bool hasState)
        {
            _compositeDisposable.Dispose();
        }

        private void UpdateStats()
        {
            OnMoveSpeedNetworkChanged();
        }

        private void OnMoveSpeedNetworkChanged()
        {
            _stats.UpdateMoveSpeed(MoveSpeedNetwork);
        }
    }
}
