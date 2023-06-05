using RTSPrototype.Abstractions;
using UnityEngine;
using Zenject;

namespace RTSPrototype.Core
{
    public class AnimatorHandler : MonoBehaviour, IPaused
    {
        [SerializeField] private Animator _animator;

        [Inject]
        private void Init(IPauseHandler pauseHandler)
        {
            pauseHandler.Register(this);
        }

        public void SetBoolAnimation(string animationName, bool value)
            => _animator.SetBool(animationName, value);


        public void SetPause(bool isPaused)
        {
            _animator.enabled = !isPaused;
        }
    }
}
