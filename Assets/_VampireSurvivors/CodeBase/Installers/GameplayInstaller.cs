using _VampireSurvivors.CodeBase.Gameplay.Knight;
using _VampireSurvivors.CodeBase.Services.Player;
using Zenject;

namespace _VampireSurvivors.CodeBase.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public Knight _knightPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle().WithArguments(_knightPrefab);
        }
    }
}
