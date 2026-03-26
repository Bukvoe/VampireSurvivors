using _VampireSurvivors.CodeBase.EntryPoints;
using _VampireSurvivors.CodeBase.Services.Network;
using _VampireSurvivors.CodeBase.Services.SceneLoad;
using Fusion;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _VampireSurvivors.CodeBase.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField, Required] private NetworkRunner _networkRunnerPrefab;

        public override void InstallBindings()
        {
            BindPrefabs();
            BindServices();

            Container.BindInterfacesTo<EntryPoint>().AsSingle();
        }

        private void BindPrefabs()
        {
            Container.Bind<NetworkRunner>().FromComponentInNewPrefab(_networkRunnerPrefab).AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.Bind<INetworkService>().To<NetworkService>().AsSingle();
            Container.Bind<ISceneLoadService>().To<SceneLoadService>().AsSingle();
        }
    }
}
