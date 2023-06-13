using System;
using RTSPrototype.Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace RTSPrototype.UIPresenter
{
    public class SelectionOutlinePresenter : MonoBehaviour
    {
        [Inject] private IObservable<ISelectable> _selected;

        private IOutlineSelector _selectedOutlineSelector;
        private ISelectable _currentSelected;

        private void Start()
        {
            _selected.Subscribe(OnSelected);
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelected == selectable)
            {
                return;
            }

            SetSelected(false);
            _selectedOutlineSelector = null;

            if (selectable != null)
            {
                _selectedOutlineSelector = (selectable as Component).GetComponentInParent<IOutlineSelector>();
                SetSelected(true);
            }
            else
            {
                if (_selectedOutlineSelector != null)
                {
                    SetSelected(false);
                }
            }

            _currentSelected = selectable;
        }

        private void SetSelected(bool state)
        {
            if (_selectedOutlineSelector != null)
            {
                _selectedOutlineSelector.ChangeState(state);
            }
        }
    }
}
