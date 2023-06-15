using UniRx;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using RTSPrototype.Abstractions;

namespace RTSPrototype.UIPresenter
{
    public class PauseMenuPresenter : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;

        [Inject] private IPauseHandler pauseHandler;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
            pauseHandler.SetPause(isActive);
        }

        private void Start()
        {
            _backButton.OnClickAsObservable().Subscribe(_ => OnBackButtonClick());
            _exitButton.OnClickAsObservable().Subscribe(_ => OnExitButtonClick());
        }

        private void OnBackButtonClick() 
        {
            gameObject.SetActive(false);
            pauseHandler.SetPause(false);
        }

        private void OnExitButtonClick() 
        {

#if UNITY_EDITOR
            Debug.Log("Exit!");
#else
            Application.Quit();
#endif

            pauseHandler.SetPause(false);
        }
    }
}
