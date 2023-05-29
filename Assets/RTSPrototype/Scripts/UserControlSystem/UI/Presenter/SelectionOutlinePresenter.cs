using RTSPrototype.Abstractions;
using RTSPrototype.UIModel;
using RTSPrototype.UIView;
using UnityEngine;

namespace RTSPrototype.UIPresenter
{
    public class SelectionOutlinePresenter : MonoBehaviour
    {
        [SerializeField] private SelectedValue _selectableValue;

        private OutlineSelector _selectedOutlineSelector;
        private ISelectable _currentSelected;

        private void Start()
        {
            _selectableValue.OnSelected += OnSelected;
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
