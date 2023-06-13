using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace RTSPrototype.Core.Unit
{
    public class UnitInstaller : MonoInstaller
    {
        [SerializeField] private string UnitId = "Unit";
        [SerializeField] private UnitAttackData _unitAttackData;

        public override void InstallBindings()
        {
            Container.Bind<IHealthHolder>().WithId(UnitId).FromComponentInChildren();
            Container.Bind<IAttackData>().WithId(UnitId).FromInstance(_unitAttackData).AsSingle();
        }
    }
}
