using _VampireSurvivors.CodeBase.Gameplay.Knight;
using _VampireSurvivors.CodeBase.Services.Network;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace _VampireSurvivors.CodeBase.Factories
{
    public class KnightFactory
    {
        private readonly Knight _knightPrefab;
        private readonly NetworkRunner _runner;

        public KnightFactory(Knight knightPrefab, NetworkRunnerProvider runnerProvider)
        {
            _knightPrefab = knightPrefab;
            _runner = runnerProvider.Runner;
        }

        public async UniTask<Knight> CreateAsync()
        {
            var networkObject = await _runner.SpawnAsync(_knightPrefab, Vector3.zero, Quaternion.identity);
            return networkObject.GetComponent<Knight>();
        }
    }
}
