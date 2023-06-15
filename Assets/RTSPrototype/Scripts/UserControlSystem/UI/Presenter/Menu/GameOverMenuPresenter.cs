using TMPro;
using UniRx;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RTSPrototype.Abstractions;
using System.Threading.Tasks;
using RTSPrototype.Utils;

namespace RTSPrototype.UIPresenter 
{
    public class GameOverMenuPresenter : MonoBehaviour
    {
        [Inject] private IGameStatus _gameStatus;

        [SerializeField] private TMP_Text _gameStateText;
        [SerializeField] private Button _exitButton;
        [SerializeField] private GameObject _view;
        [SerializeField] private float _gameEndDelay = 1f;

        private bool _isGameOver;

        [Inject]
        private void Init()
        {
            _gameStatus.Status.ObserveOnMainThread().Subscribe(OnGameOver);
            _exitButton.onClick.AddListener(QuitGame);
        }

        private void OnGameOver(int result)
        {
            if (_isGameOver) return;

            StartGameOverLogic(result);
        }

        private async void StartGameOverLogic(int result)
        {
            _isGameOver = true;

            await Task.Delay((int)_gameEndDelay * Const.TaskTimeCoeff);
            
            string resultText = result == 0 
                ? "Draw!" 
                : $"Faction №{result} WIN!";

            _view.SetActive(true);
            _gameStateText.text = resultText;
            Time.timeScale = 0;
        }

        public void QuitGame()
        {
            Time.timeScale = 1;

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
