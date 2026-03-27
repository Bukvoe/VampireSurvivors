using System.ComponentModel.DataAnnotations;
using _VampireSurvivors.CodeBase.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace _VampireSurvivors.CodeBase.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField, Required] private MainMenuView _mainMenuViewPrefab;

        public override void InstallBindings()
        {
            Container.Bind<MainMenuView>().FromComponentInNewPrefab(_mainMenuViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();
        }
    }
}
