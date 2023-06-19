using UniRx;
using System;
using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;
using RTSPrototype.Abstractions;
using RTSPrototype.UIModel.CommandRealization;

namespace RTSPrototype.UIPresenter
{
    public class UnitInputPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _groundTransform;

        private Plane _groundPlane;

        [Inject] private IObservable<ISelectable> _selected;

        private IUnit _currentUnit;
        private IDisposable _currentUnitEvent;

        private void Start()
        {
            _selected.Subscribe(OnSelected);
            _groundPlane = new Plane(_groundTransform.up, 0);
        }

        private void OnSelected(ISelectable selected)
        {
            var unit = (selected as Component) as IUnit;

            if (unit != null)
            {
                _currentUnitEvent = Observable
                                .EveryUpdate()
                                .Where(_ => Input.GetMouseButtonUp(1) && !EventSystem.current.IsPointerOverGameObject())
                                .Select(_ => _camera.ScreenPointToRay(Input.mousePosition))
                                .Select(ray => (ray, Physics.RaycastAll(ray)))
                                .Subscribe(rayData =>
                                {
                                    var (ray, hits) = rayData;
                                    OnRightMouseClick(ray, hits);
                                });

                _currentUnit = unit;
            }
            else if (_currentUnitEvent != null)
            {
                _currentUnitEvent.Dispose();

                _currentUnit = default;
                _currentUnit = default;
            }
        }

        private void OnRightMouseClick(Ray ray, RaycastHit[] hits)
        {
            if (_currentUnit == null) return;

            if(_groundPlane.Raycast(ray, out var enter))
            {
                var queue = (_currentUnit as Component).GetComponent<ICommandsQueue>();

                if(queue.CurrentCommandInQueue == 0)
                {
                    queue.EnqueueCommand(new MoveCommand(ray.origin + ray.direction * enter));
                }
            }
        }
    }
}
