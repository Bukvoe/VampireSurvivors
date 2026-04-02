using _VampireSurvivors.CodeBase.Config;
using _VampireSurvivors.CodeBase.Gameplay.Hero;
using _VampireSurvivors.CodeBase.Services.Network;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Factories
{
    public class HeroFactory
    {
        private readonly Hero _heroPrefab;
        private readonly NetworkRunner _runner;
        private readonly HeroStatsConfig _heroStatsConfig;

        public HeroFactory(Hero heroPrefab, NetworkRunnerProvider runnerProvider, HeroStatsConfig heroStatsConfig)
        {
            _heroPrefab = heroPrefab;
            _runner = runnerProvider.Runner;
            _heroStatsConfig = heroStatsConfig;
        }

        public async UniTask<Hero> CreateAsync()
        {
            var networkObject = await _runner.SpawnAsync(
                _heroPrefab,
                Vector3.zero,
                Quaternion.identity,
                onBeforeSpawned: OnBeforeSpawned);

            var hero = networkObject.GetComponent<Hero>();

            return hero;
        }

        private void OnBeforeSpawned(NetworkRunner runner, NetworkObject networkObject)
        {
            if (!networkObject.TryGetComponent(out HeroStatsNetwork statsNetwork))
            {
                Debug.LogError($"{nameof(Hero)} does not have a {nameof(HeroStatsNetwork)} component.");
                return;
            }

            statsNetwork.BeforeSpawned(_heroStatsConfig);
        }
    }
}
