using System;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;
using RTSPrototype.Abstractions;
using UnityEngine.UI;

namespace RTSPrototype.UIPresenter
{
    public class UITopPannelPresenter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _menuButton;
        [SerializeField] private PauseMenuPresenter _pauseMenuGo;

        private void Awake()
        {
            _pauseMenuGo.SetActive(false);
        }

        [Inject]
        private void Init(ITimeModel timeModel)
        {
            timeModel.GameTime.Subscribe(seconds =>
            {
                var time = TimeSpan.FromSeconds(seconds);
                _inputField.text = string.Format(
                    "{0:D2}:{1:D2}", 
                    time.Minutes, 
                    time.Seconds);
            });

            _menuButton.OnClickAsObservable()
                .Subscribe(_ => _pauseMenuGo.SetActive(true));

        }
    }
}
