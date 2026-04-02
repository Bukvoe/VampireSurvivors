using _VampireSurvivors.CodeBase.Config;
using _VampireSurvivors.CodeBase.Factories;
using _VampireSurvivors.CodeBase.Gameplay.Hero;
using _VampireSurvivors.CodeBase.Services.Input;
using _VampireSurvivors.CodeBase.Services.Input.Providers;
using _VampireSurvivors.CodeBase.Services.Player;
using _VampireSurvivors.CodeBase.UI.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _VampireSurvivors.CodeBase.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField, Required] private Hero _heroPrefab;
        [SerializeField, Required] private HeroStatsConfig _heroStatsConfig;
        [SerializeField, Required] private StatsView _statsViewPrefab;

        public override void InstallBindings()
        {
            BindFactories();
            BindServices();
            BindUI();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<HeroFactory>().AsSingle().WithArguments(_heroPrefab, _heroStatsConfig);
        }

        private void BindServices()
        {
            Container.Bind<InputActions>().AsSingle();
            Container.BindInterfacesTo<KeyboardInputProvider>().AsSingle();
            Container.Bind<IInputProvider>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputService>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<StatsView>().FromComponentInNewPrefab(_statsViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<StatsPresenter>().AsSingle();
        }
    }
}
