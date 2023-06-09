﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;


namespace RTSPrototype.Abstractions.ScriptableObjects
{
    public interface IAssetContext
    {
        Object GetObjectOfType(Type targetType, string targetName = null);
    }

    [CreateAssetMenu(fileName = nameof(AssetsContext), menuName = "RTSPrototype/" + nameof(AssetsContext))]
    public class AssetsContext : ScriptableObject, IAssetContext
    {
        [SerializeField] private Object[] _assetObjects;

        public Object GetObjectOfType(Type targetType, string targetName = null)
        {
            for (int i = 0; i < _assetObjects.Length; i++)
            {
                var obj = _assetObjects[i];
                if (obj.GetType().IsAssignableFrom(targetType))
                {
                    if (targetName == null || obj.name == targetName)
                    {
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}
