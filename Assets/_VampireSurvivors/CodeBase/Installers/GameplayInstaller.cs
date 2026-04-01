using _VampireSurvivors.CodeBase.Factories;
using _VampireSurvivors.CodeBase.Gameplay.Knight;
using _VampireSurvivors.CodeBase.Services.Input;
using _VampireSurvivors.CodeBase.Services.Input.Providers;
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
            Container.Bind<InputActions>().AsSingle();
            Container.BindInterfacesTo<KeyboardInputProvider>().AsSingle();
            Container.Bind<IInputProvider>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputService>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();
        }
    }
}
