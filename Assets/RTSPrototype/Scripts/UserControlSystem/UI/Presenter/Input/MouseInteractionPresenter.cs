using UniRx;
using Zenject;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using RTSPrototype.Abstractions;

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

            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
                .Select(_ => _camera.ScreenPointToRay(Input.mousePosition))
                .Select(ray => Physics.RaycastAll(ray))
                .Subscribe(hits =>
                {
                    OnLeftMouseClick(hits);
                });

            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonUp(1) && !EventSystem.current.IsPointerOverGameObject())
                .Select(_ => _camera.ScreenPointToRay(Input.mousePosition))
                .Select(ray => (ray, Physics.RaycastAll(ray)))
                .Subscribe(rayData =>
                {
                    var (ray, hits) = rayData;
                    OnRightMouseClick(ray, hits);
                });

        }

        private void OnLeftMouseClick(RaycastHit[] hits)
        {
            if (CheckForHit<ISelectable>(hits, out var selectable))
            {
                _selectedLMB.SetValue(selectable);
            }
            else
            {
                _selectedLMB.SetValue(null);
            }
        }
        private void OnRightMouseClick(Ray ray, RaycastHit[] hits)
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
