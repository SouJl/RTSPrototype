using RTSPrototype.Abstractions;
using RTSPrototype.UIModel;
using UnityEngine;

namespace RTSPrototype.UIPresenter
{
    public class SelectedOutlinePresenter :  MonoBehaviour
    {
        [Header("Outline")]
        [SerializeField] private Outline _outline;
        [Header("Selected Value")]
        [SerializeField] private SelectedValue _selectedObject;

        private void Start()
        {
            _selectedObject.OnSelected += OnSelected;
            OnSelected(_selectedObject.CurrentValue);
        }

        private void OnSelected(ISelectable selectable)
        {
            _outline.enabled = selectable != null;
        }
    }
}
