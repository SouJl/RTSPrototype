using Zenject;
using UnityEngine;

namespace RTSPrototype.Abstractions.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Installers/AssetsInstaller")]
    public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
    {
        [SerializeField] private AssetsContext _legacyContext;

        public override void InstallBindings()
        {
            Container.Bind<IAssetContext>().FromInstance(_legacyContext);
        }
    }
}

