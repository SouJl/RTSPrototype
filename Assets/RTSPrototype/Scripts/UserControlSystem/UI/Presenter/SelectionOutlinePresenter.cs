using UnityEngine;
using UnityEngine.EventSystems;

namespace RTSPrototype.UIPresenter
{
    public class SelectionOutlinePresenter :  MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private Collider _currentSelection;

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!Input.GetMouseButtonDown(0))
                return;

            Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit);

            var hitSelection = raycastHit.collider;

            if (hitSelection && hitSelection.tag == "Selected")
            {
                if (_currentSelection != null)
                {
                    _currentSelection.GetComponentInParent<Outline>().enabled = false;
                }
                _currentSelection = raycastHit.collider;
                _currentSelection.GetComponentInParent<Outline>().enabled = true;
            }
            else 
            {
                if (_currentSelection)
                {
                    _currentSelection.GetComponentInParent<Outline>().enabled = false;
                    _currentSelection = null;
                }
            }
        }
    }
}
