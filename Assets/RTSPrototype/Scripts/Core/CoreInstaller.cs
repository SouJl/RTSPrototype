using Zenject;
using UnityEngine;
using RTSPrototype.Abstractions;
using RTSPrototype.Core.AdditionalComponents;

namespace RTSPrototype.Core
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private GameStatus _gameStatus;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseHandlerModel>().AsSingle().NonLazy();
            Container.Bind<IFactionManager>().To<FactionManager>().AsSingle().NonLazy();

            Container.Bind<IGameStatus>().FromInstance(_gameStatus).AsSingle();
        }
    }
}
