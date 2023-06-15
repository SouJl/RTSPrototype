using Zenject;
using UnityEngine;

namespace RTSPrototype.UIView
{
    public class UiViewInstaller : MonoInstaller
    {
        [SerializeField] private CommandButtonsView _cmdButtonsView;
        [SerializeField] private ProduceUnitsUIView _produceView;

        public override void InstallBindings()
        {
            Container.Bind<CommandButtonsView>().FromInstance(_cmdButtonsView).AsSingle();
            Container.Bind<ProduceUnitsUIView>().FromInstance(_produceView).AsSingle();
        }
    }
}
