using Zenject;

namespace _VampireSurvivors.CodeBase.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EntryPoint>().AsSingle();
        }
    }
}
