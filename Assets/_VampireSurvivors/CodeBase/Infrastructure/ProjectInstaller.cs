using _VampireSurvivors.CodeBase.Services.SceneLoad;
using Zenject;

namespace _VampireSurvivors.CodeBase.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();

            Container.BindInterfacesTo<EntryPoint>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<ISceneLoadService>().To<SceneLoadService>().AsSingle();
        }
    }
}
