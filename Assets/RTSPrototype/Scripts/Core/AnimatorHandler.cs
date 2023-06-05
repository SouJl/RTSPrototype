using RTSPrototype.Abstractions;
using UniRx;
using UnityEngine;
using Zenject;

namespace RTSPrototype.Core
{
    public class AnimatorHandler : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [Inject] private IPauseHandler _pauseHandler;

        private void Start()
        {
            _pauseHandler.IsPaused.Subscribe(OnPause);
        }

        public void SetBoolAnimation(string animationName, bool value)
            => _animator.SetBool(animationName, value);


        private void OnPause(bool isPaused)
        {
            _animator.enabled = !isPaused;
        }
    }
}
