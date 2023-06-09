using RTSPrototype.Abstractions.ScriptableObjects;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AssetsInstaller", menuName = "Installers/AssetsInstaller")]
public class AssetsInstaller : ScriptableObjectInstaller<AssetsInstaller>
{
    [SerializeField] private AssetsContext _legacyContext;

    public override void InstallBindings()
    {
        Container.Bind<IAssetContext>().FromInstance(_legacyContext);
    }
}