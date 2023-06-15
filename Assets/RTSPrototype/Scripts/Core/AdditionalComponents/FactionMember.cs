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

        private IOutlineSelector _outlineSelector;

        private void OnEnable()
        {
            _outlineSelector ??= GetComponent<OutlineSelector>();
        }

        private void Start()
        {
            _outlineSelector.ChangeColor(FactionColor);
        }


        public void SetFactionColor(Color factionColor)
        {
            _factionColor = factionColor;
        }

        public void SetFactionId(int factionId)
        {
            _factionId = factionId;
        }
    }
}
