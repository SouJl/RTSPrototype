using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.UIModel.CommandCreators;
using UnityEngine;
using Zenject;

namespace RTSPrototype.UIModel
{
    public class UIModelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {          
            BindingValues();

            BindingCommands();

            Container.Bind<CommandButtonsModel>().AsSingle();
        }

        private void BindingValues()
        {
            Container.Bind<IRTSValue<ISelectable>>().To<SelectedValue>().AsSingle().NonLazy();
            Container.Bind<IRTSValue<Vector3>>().To<Vector3Value>().AsSingle().NonLazy();
            Container.Bind<IRTSValue<IAttackable>>().To<AttackableValue>().AsSingle().NonLazy();
        }

        private void BindingCommands()
        {
            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCommandCreator>().AsSingle();
        }
    }
}
