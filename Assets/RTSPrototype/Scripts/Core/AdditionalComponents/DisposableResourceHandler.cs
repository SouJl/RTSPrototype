using System;
using UnityEngine;
using System.Collections.Generic;
using RTSPrototype.Utils.Attributes;
using System.Linq;

namespace RTSPrototype.Core.AdditionalComponents
{
    public class DisposableResourceHandler : MonoBehaviour, IDisposable
    {
        private readonly List<IDisposable> _resources = new List<IDisposable>();

        private enum State
        {
            Editor,
            Work,
            Disable,
            Warning
        }
 
        [SerializeField, ReadOnly] private State _state = State.Editor;
        [SerializeField, ReadOnly] private int _disposableCount;
        [Space(5)]
        [SerializeField] private bool _acitveDispose = true;


        private void Awake()
        {
            if (_acitveDispose == false) 
            {
                _state = State.Disable;
                return;
            }

            _resources
                .AddRange(
                GetComponentsInChildren<IDisposable>()
                .Where(d => d as DisposableResourceHandler == null)
                .ToList());

            if (_resources.Count == 0)
            {
                Debug.LogWarning($"DisposableResourceHandler can't find any IDisposable on {gameObject.name}");
                _state = State.Warning;
                _acitveDispose = false;
                return;
            }

            _state = State.Work;
            _disposableCount = _resources.Count;
        }

        public void Dispose()
        {
            if (_acitveDispose == false) return;

            _resources.ForEach(d => d.Dispose());
            _resources.Clear();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
