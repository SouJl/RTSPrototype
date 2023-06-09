using System;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.ScriptableObjects;
using UnityEngine;

namespace RTSPrototype.Core
{
    public class UnitProductionTask : IUnitProductionTask
    {
        public string UnitName { get; }
        public float ProductionTime { get; }
        public Sprite Icon { get; }
        public GameObject UnitPrefab { get; }

        public float TimeLeft { get; set; }

        public UnitProductionTask(IProducedUnitData data)
        {
            if (data == null) 
                throw new ArgumentNullException(nameof(data));
           
            UnitName = data.UnitName;
            ProductionTime = data.ProductionTime;
            Icon = data.Icon;
            UnitPrefab = data.UnitPrefab;


            TimeLeft = ProductionTime;
        }
    }
}
