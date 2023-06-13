using RTSPrototype.Abstractions;
using UnityEngine;

namespace RTSPrototype.Core.AdditionalComponents
{
    public class OutlineSelector : MonoBehaviour, IOutlineSelector
    {
        [SerializeField] private bool _isOutlineEnable= true;
        [SerializeField] private Outline[] _onGameobjectOutlines;

        private bool _isActive;

        private void Awake()
        {
            if (_onGameobjectOutlines.Length == 0) 
            {
                _isOutlineEnable = false;
                Debug.Log($"Outline component not present in {gameObject.name}!" +
                    $"\n Object will not be outlined.");

                return;
            }

            _isActive = false;
        }


        private void Start() => 
            SwitchOutlinesState(_isActive);
       
        public void ChangeState(bool outlineState) 
        {
            if (!_isOutlineEnable) return;

            if(_isActive == outlineState) 
            {
                return;
            }

            _isActive = outlineState;

            SwitchOutlinesState(_isActive);
        }

        private void SwitchOutlinesState(bool toChnageState)
        {
            for (int i = 0; i < _onGameobjectOutlines.Length; i++)
            {
                _onGameobjectOutlines[i].enabled = toChnageState;
            }
        }

        public void ChangeColor(Color newColor)
        {
            if (!_isOutlineEnable) return;

            for (int i = 0; i < _onGameobjectOutlines.Length; i++)
            {
                _onGameobjectOutlines[i].OutlineColor = newColor;
            }
        }
    }
}
