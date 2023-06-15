using Zenject;
using UnityEngine;
using RTSPrototype.Abstractions;
using RTSPrototype.UIModel.CommandCreators;
using RTSPrototype.Abstractions.ScriptableObjects;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.UIModel
{
    public class UIModelInstaller : MonoInstaller
    {
        [SerializeField] private ProducedUnitData _producedUnitData;

        public override void InstallBindings()
        {
            Container.Bind<IProducedUnitData>().WithId("Knight").FromInstance(_producedUnitData);

            BindingValues();

            BindingCommands();

            Container.Bind<CommandButtonsModel>().AsSingle();
            Container.Bind<ProduceUnitsUIModel>().AsSingle();
        }

        private void BindingValues()
        {
            Container.BindInterfacesAndSelfTo<SelectedValue>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Vector3Value>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AttackableValue>().AsSingle().NonLazy();
        }

        private void BindingCommands()
        {
            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCommandCreator>().AsSingle();

            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCommandCreator>().AsSingle();
            Container.Bind<CommandCreatorBase<ISetRallyPointCommand>>().To<SetRallyPointCommandCreator>().AsSingle();
        }
    }
}
