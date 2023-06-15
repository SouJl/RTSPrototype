using Zenject;
using UnityEngine;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core.AdditionalComponents
{
    [RequireComponent(typeof(OutlineSelector))]
    public class FactionMember : MonoBehaviour, IFactionMember
    {
        public int FactionId => _factionId;
        public Color FactionColor => _factionColor;

        [SerializeField] private int _factionId;
        [SerializeField] private Color _factionColor = Color.red;

        [Inject] private IFactionManager _factionManager;

        private IOutlineSelector _outlineSelector;
        private int _selfId;

        private void OnEnable()
        {
            _selfId = GetInstanceID();
            _outlineSelector ??= GetComponent<OutlineSelector>();
        }

        private void Start()
        {
            _outlineSelector.ChangeColor(FactionColor);
            if (_factionId != 0)
            {
                _factionManager.Register(_factionId, _selfId);
            }
        }

        public void SetFactionColor(Color factionColor)
        {
            _factionColor = factionColor;
        }

        public void SetFactionId(int factionId)
        {
            _factionId = factionId;
        }

        private void OnDestroy()
        {
            _factionManager.Unregister(_factionId, _selfId);
        }
    }
}
