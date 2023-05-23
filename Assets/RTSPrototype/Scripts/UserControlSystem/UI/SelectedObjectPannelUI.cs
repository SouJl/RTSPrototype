using UnityEngine;
using UnityEngine.UI;
using RTSPrototype.Abstractions;

namespace RTSPrototype.UserControlSystem.UI
{
    public class SelectedObjectPannelUI : MonoBehaviour
    {
        [SerializeField] private Image _selectedImage;
        [SerializeField] private SelectedValue _selectedObject;

        private void Start()
        {
            _selectedObject.OnSelected += OnSelected;
            OnSelected(_selectedObject.CurrentValue);
        }

        private void OnSelected(ISelectable selected)
        {
            _selectedImage.enabled = selected != null;
            
            if(selected != null)
            {
                _selectedImage.sprite = selected.Icon;
            }
        }
    }
}
