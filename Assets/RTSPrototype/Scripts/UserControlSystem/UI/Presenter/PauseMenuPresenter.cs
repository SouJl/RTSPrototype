using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace RTSPrototype.UIPresenter
{
    public class PauseMenuPresenter : MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;


        private void Start()
        {
            _backButton.OnClickAsObservable().Subscribe(_ => OnBackButtonClick());
            _exitButton.OnClickAsObservable().Subscribe(_ => OnExitButtonClick());
        }


        private void OnBackButtonClick() 
        {
            gameObject.SetActive(false);
        }

        private void OnExitButtonClick() 
        {

#if UNITY_EDITOR
            Debug.Log("Exit!");
#else
            Application.Quit();
#endif
        }
    }
}
