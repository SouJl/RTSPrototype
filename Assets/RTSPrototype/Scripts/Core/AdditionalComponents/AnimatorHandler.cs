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

        public void SetBoolAnimation(string name, bool value)
        {
            _animator.SetBool(name, value);
        }

        public void SetTriggerAnimation(string trigger)
        {
            _animator.SetTrigger(trigger);
        }
        
        public float GetCurrentAnimationLength()
        {
            return _animator
                .GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }

        private void OnPause(bool isPaused)
        {
            if (this == null) return;

            _animator.enabled = !isPaused;
        }
    }
}
