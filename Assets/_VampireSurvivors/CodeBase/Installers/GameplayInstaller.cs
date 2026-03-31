using _VampireSurvivors.CodeBase.Factories;
using _VampireSurvivors.CodeBase.Gameplay.Knight;
using _VampireSurvivors.CodeBase.Services.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _VampireSurvivors.CodeBase.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField, Required] private Knight _knightPrefab;

        public override void InstallBindings()
        {
            BindFactories();
            BindServices();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<KnightFactory>().AsSingle().WithArguments(_knightPrefab);
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();
        }
    }
}
