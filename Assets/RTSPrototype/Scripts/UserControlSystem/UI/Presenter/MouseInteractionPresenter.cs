using System.Linq;
using UnityEngine;
using RTSPrototype.Abstractions;
using UnityEngine.EventSystems;
using Zenject;

namespace RTSPrototype.UIPresenter 
{
    public class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [Header("Get OnClick Position Settings")]
        [SerializeField] private Transform _groundTransform;

        [Inject] private IRTSValue<ISelectable> _selectedLMB;
        [Inject] private IRTSValue<Vector3> _groundClicksRMB;
        [Inject] private IRTSValue<IAttackable> _attackablesRMB;


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
            var hits = Physics.RaycastAll(ray);

            if (Input.GetMouseButtonUp(0))
            {
                if (CheckForHit<ISelectable>(hits, out var selectable)) 
                {
                    _selectedLMB.SetValue(selectable);
                }
            }            
            else if(Input.GetMouseButton(1))
            {
                if (CheckForHit<IAttackable>(hits, out var attackable))
                {
                    _attackablesRMB.SetValue(attackable);
                }
                else if (_groundPlane.Raycast(ray, out var enter)) 
                {
                    _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
                }
            }

        }

        private bool CheckForHit<THit>(RaycastHit[] hits, out THit result) where THit :class
        {
            result = default;
            if (hits.Length == 0)
            {
                return false;
            }
            result = hits
                .Select(hit => hit.collider.GetComponentInParent<THit>())
                .Where(c => c != null)
                .FirstOrDefault();

            return result != default;
        }
    }
}
