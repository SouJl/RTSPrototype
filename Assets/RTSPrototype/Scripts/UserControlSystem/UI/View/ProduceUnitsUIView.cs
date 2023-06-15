using UniRx;
using TMPro;
using System;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using RTSPrototype.Abstractions;

namespace RTSPrototype.UIView
{
    public class ProduceUnitsUIView : MonoBehaviour
    {
        public IObservable<int> CancelButtonClicks => _cancelButtonClicks;

        [SerializeField] private Image _currentProducedIcon;
        [SerializeField] private Slider _productionProgressSlider;
        [SerializeField] private TextMeshProUGUI _currentUnitName;
        [SerializeField] private Image[] _images;
        [SerializeField] private GameObject[] _imageHolders;
        [SerializeField] private Button[] _buttons;


        private readonly Color _noColor = new Color(0, 0, 0, 0);
        private readonly Color _fullColor = new Color(255, 255, 255, 255);

        private Subject<int> _cancelButtonClicks = new Subject<int>();
        private IDisposable _unitProductionTaskCt;

        [Inject]
        private void Init()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                var index = i;
                _buttons[i].onClick.AddListener(() => _cancelButtonClicks.OnNext(index));
            }

            _currentProducedIcon.color = _noColor;
        }

        public void AddTask(IUnitProductionTask task, int index)
        {
            _currentProducedIcon.color = _fullColor;
            _currentProducedIcon.sprite = task.Icon;

            _imageHolders[index].SetActive(true);
            _images[index].sprite = task.Icon;

            if (index == 0)
            {
                _productionProgressSlider.gameObject.SetActive(true);
                _currentUnitName.text = task.UnitName;
                _currentUnitName.enabled = true;
                _unitProductionTaskCt = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    _productionProgressSlider.value = task.TimeLeft / task.ProductionTime;
                });
            }

        }

        public void RemoveTask(int index)
        {
            _imageHolders[index].SetActive(false);
            _images[index].sprite = null;

            if (index == 0)
            {
                _currentProducedIcon.sprite = null;
                _currentProducedIcon.color = _noColor;

                _productionProgressSlider.gameObject.SetActive(false);
                _currentUnitName.text = string.Empty;
                _currentUnitName.enabled = false;
                _unitProductionTaskCt?.Dispose();
            }
        }

        public void ReplaceTask(IUnitProductionTask task, int index) 
        {
            AddTask(task, index);
        }

        public void Clear()
        {
            _currentProducedIcon.sprite = null;
            _currentProducedIcon.color = _noColor;

            for (int i = 0; i < _images.Length; i++)
            {
                _imageHolders[i].SetActive(false);
                _images[i].sprite = null;
            }

            _productionProgressSlider.gameObject.SetActive(false);
            _currentUnitName.text = string.Empty;
            _currentUnitName.enabled = false;
            _unitProductionTaskCt?.Dispose();
        }


    }
}
