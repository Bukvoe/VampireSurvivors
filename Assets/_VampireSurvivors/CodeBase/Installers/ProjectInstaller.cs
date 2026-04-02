using _VampireSurvivors.CodeBase.EntryPoints;
using _VampireSurvivors.CodeBase.Ineteractors;
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
            BindServices();

            BindInteractors();

            Container.BindInterfacesTo<EntryPoint>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<FusionCallbacks>().AsSingle();
            Container.Bind<NetworkRunnerProvider>().AsSingle().WithArguments(_networkRunnerPrefab);
            Container.BindInterfacesTo<NetworkService>().AsSingle();
            Container.Bind<ISceneLoadService>().To<SceneLoadService>().AsSingle();
        }

        private void BindInteractors()
        {
            Container.BindInterfacesTo<HandleDisconnectInteractor>().AsSingle();
        }
    }
}
