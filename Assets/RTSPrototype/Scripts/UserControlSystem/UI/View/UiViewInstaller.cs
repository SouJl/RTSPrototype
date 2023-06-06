using UnityEngine;
using Zenject;

namespace RTSPrototype.UIView
{
    public class UiViewInstaller : MonoInstaller
    {
        [SerializeField] private ProduceUnitsUIView _produceView;

        public override void InstallBindings()
        {
            Container.Bind<ProduceUnitsUIView>().FromInstance(_produceView).AsSingle();
        }
    }
}
