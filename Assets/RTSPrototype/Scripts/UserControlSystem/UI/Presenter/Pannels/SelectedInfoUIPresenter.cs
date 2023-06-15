using UniRx;
using TMPro;
using System;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using RTSPrototype.Abstractions;

namespace RTSPrototype.UIPresenter
{
    public class SelectedInfoUIPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _selectedName;

        [Header("Icon Settings")]
        [SerializeField] private GameObject _iconObject;
        [SerializeField] private Image _selectedImage;

        [Header("Health Settings")]
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Image _healtSliderBack;
        [SerializeField] private Image _healthSliderFill;
        [SerializeField] private TMP_Text _healthTextValue;

        [Header("Health present Swttings")]
        [SerializeField] private Color _minRangeColor = Color.red;
        [SerializeField] private Color _maxRangeColor = Color.green;
        [SerializeField] private float _colorSmooth = 0.5f;

        [Inject] private IObservable<ISelectable> _selected;

        private void Start()
        {
            _selected.Subscribe(OnSelected);
        }

        private void OnSelected(ISelectable selected)
        {
            bool selectedState = selected != null;

            _selectedName.enabled = selectedState;

            _iconObject.SetActive(selectedState);
            _selectedImage.enabled = selectedState;

            _healthTextValue.enabled = selectedState;
            _healthSlider.gameObject.SetActive(selectedState);

            if(selected != null)
            {
                _selectedName.text = selected.Name;

                _selectedImage.sprite = selected.Icon;

                SetupHealth(selected);

                _healthTextValue.text = $"{selected.CurrentHealth}/{selected.MaxHealth}";
            }
        }

        private void SetupHealth(ISelectable selected)
        {
            _healthSlider.minValue = 0;
            _healthSlider.maxValue = selected.MaxHealth;
            _healthSlider.value = selected.CurrentHealth;

            var color = Color.Lerp(
                _minRangeColor, 
                _maxRangeColor, 
                selected.CurrentHealth / (float)selected.MaxHealth);

            _healtSliderBack.color = color * _colorSmooth;
            _healthSliderFill.color = color;
        }
    }
}
