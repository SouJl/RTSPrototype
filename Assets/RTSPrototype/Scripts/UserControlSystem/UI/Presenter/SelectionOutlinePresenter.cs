﻿using RTSPrototype.Abstractions;
using RTSPrototype.UIView;
using UnityEngine;
using Zenject;

namespace RTSPrototype.UIPresenter
{
    public class SelectionOutlinePresenter : MonoBehaviour
    {
        [Inject] private RTSValueBase<ISelectable> _selectableValue;

        private OutlineSelector _selectedOutlineSelector;
        private ISelectable _currentSelected;

        private void Start()
        {
            _selectableValue.OnNewValue += OnSelected;
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
                _selectedOutlineSelector = (selectable as Component).GetComponentInParent<OutlineSelector>();
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
