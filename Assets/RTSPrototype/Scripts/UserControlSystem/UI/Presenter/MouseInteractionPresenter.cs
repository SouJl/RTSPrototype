using System.Linq;
using UnityEngine;
using RTSPrototype.UIModel;
using RTSPrototype.Abstractions;
using UnityEngine.EventSystems;

namespace RTSPrototype.UIPresenter 
{
    public class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectedValue _selectedObject;
        [SerializeField] private EventSystem _eventSystem;

        private void Update()
        {
            if (_eventSystem.IsPointerOverGameObject())
                return;

            if (!Input.GetMouseButtonUp(0)) 
                return;

            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));

            if (hits.Length == 0) 
                return;

            var selected= hits
                .Select(hit => hit.collider.GetComponentInParent<ISelectable>())
                .Where(b => b != null)
                .FirstOrDefault();
            _selectedObject.SetValue(selected);
        }
    }
}
