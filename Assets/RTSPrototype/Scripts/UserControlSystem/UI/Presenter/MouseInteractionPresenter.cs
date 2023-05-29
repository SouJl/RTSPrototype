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

        [Header("Selection Settings")]
        [SerializeField] private SelectedValue _selectedObject;

        [Header("Get OnClick Position Settings")]
        [SerializeField] private Vector3Value _groundClicksRMB;
        [SerializeField] private Transform _groundTransform;

        private Plane _groundPlane;

        private void Start()
        {
            _groundPlane = new Plane(_groundTransform.up, 0);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
            {
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            var ray = _camera.ScreenPointToRay(Input.mousePosition);
           
            if (Input.GetMouseButtonUp(0))
            {
                var hits = Physics.RaycastAll(ray);
                
                if (hits.Length == 0)
                {
                    return;
                }
                var selectable = hits
                    .Select(hit =>hit.collider.GetComponentInParent<ISelectable>())
                    .Where(c => c != null)
                    .FirstOrDefault();

                _selectedObject.SetValue(selectable);
            }

            if(Input.GetMouseButton(1))
            {
                if (_groundPlane.Raycast(ray, out var enter))
                {
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
                }
            }

        }
    }
}
