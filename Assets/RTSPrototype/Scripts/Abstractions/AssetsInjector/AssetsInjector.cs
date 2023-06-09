﻿using System;
using System.Reflection;
using RTSPrototype.Abstractions.ScriptableObjects;

namespace RTSPrototype.Abstractions.AssetsInjector
{
    public static class AssetsInjector
    {
        private static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);

        public static T Inject<T>(this IAssetContext context, T target)
        {
            var targetType = target.GetType();

            do
            {
                var targetFields = targetType.GetFields(
                    BindingFlags.NonPublic |
                    BindingFlags.Public |
                    BindingFlags.Instance);

                for (int i = 0; i < targetFields.Length; i++)
                {
                    var fieldInfo = targetFields[i];
                    var injectAssetAttribute
                        = fieldInfo.GetCustomAttribute(_injectAssetAttributeType) as InjectAssetAttribute;
                    if (injectAssetAttribute == null)
                    {
                        continue;
                    }
                    var objectToInject = context.GetObjectOfType(fieldInfo.FieldType, injectAssetAttribute.AssetName);
                    fieldInfo.SetValue(target, objectToInject);
                }

                targetType = targetType.BaseType;
            }
            while (targetType != null);

            return target;
        }
    }

}
