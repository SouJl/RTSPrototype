using TMPro;
using UniRx;
using Zenject;
using UnityEngine;
using System.Text;
using RTSPrototype.Abstractions;

namespace RTSPrototype.UIPresenter 
{
    public class GameOverMenuPresenter : MonoBehaviour
    {
        [Inject] private IGameStatus _gameStatus;

        [SerializeField] private TMP_Text _gameStateText;
        [SerializeField] private GameObject _view;


        [Inject]
        private void Init()
        {
            _gameStatus.Status.ObserveOnMainThread().Subscribe(result =>
            {
                string resultText = "";
                if (result == 0)
                {
                    resultText = "Draw!";
                }
                else
                {
                    resultText = $"Faction №{result} WIN!";
                }
                _view.SetActive(true);
                _gameStateText.text = resultText;
                Time.timeScale = 0;
            });
        }

    }
}
