using UniRx;
using System;
using Zenject;
using UnityEngine;
using RTSPrototype.Abstractions;

namespace RTSPrototype.UIModel
{
    public class ProduceUnitsUIModel
    {
        public IObservable<IUnitProducer> UnitProducers { get; private set; }
        
        [Inject]
        public void Init(IObservable<ISelectable> currentlySelected)
        {
            UnitProducers = currentlySelected
                .Select(selectable => selectable as Component)
                .Select(component => component?.GetComponent<IUnitProducer>());
        }
    }
}
