using _VampireSurvivors.CodeBase.Services.Player;
using Zenject;

namespace _VampireSurvivors.CodeBase.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();
        }
    }
}
