using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.UIModel.CommandCreators;
using RTSPrototype.Utils;
using UnityEngine;
using Zenject;

namespace RTSPrototype.UIModel
{
    public class UIModelInstaller : MonoInstaller
    {
        [SerializeField] private AssetsContext _legacyContext;

        public override void InstallBindings()
        {
            Container.Bind<AssetsContext>().FromInstance(_legacyContext);

            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCommandCreator>().AsSingle();
            
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCommandCreator>().AsSingle();
            
            Container.Bind<CommandButtonsModel>().AsSingle();
        }

    }
}
