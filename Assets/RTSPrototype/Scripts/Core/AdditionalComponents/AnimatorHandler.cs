using UniRx;
using System;
using Zenject;
using UnityEngine;
using RTSPrototype.Abstractions;

namespace RTSPrototype.Core
{
    public enum AnimationType
    {
        Idle,
        Walk,
        Attack,
        Death
    }

    public class AnimatorHandler : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        [Inject] private IPauseHandler _pauseHandler;

        private readonly string IDLE_NAME   = "Idle";
        private readonly string WALK_NAME   = "Walk";
        private readonly string ATTACK_NAME = "Attack";
        private readonly string DEATH_NAME  = "Death";

        private IDisposable _pauseEvent;
      
        private void Start()
        {
            _pauseEvent = _pauseHandler.IsPaused.Subscribe(OnPause);
        }

        private void OnDestroy()
        {
            _pauseEvent.Dispose();
        }

        public void ChangeState(AnimationType type)
        {
            switch (type)
            {
                case AnimationType.Idle:
                    {
                        SetBoolAnimation(IDLE_NAME, true);
                        SetBoolAnimation(WALK_NAME, false);
                        SetBoolAnimation(ATTACK_NAME, false);
                        SetBoolAnimation(DEATH_NAME, false);
                        break;
                    }
                case AnimationType.Walk:
                    {

                        SetBoolAnimation(WALK_NAME, true);
                        SetBoolAnimation(IDLE_NAME, false);
                        SetBoolAnimation(ATTACK_NAME, false);
                        SetBoolAnimation(DEATH_NAME, false);
                        break;
                    }
                case AnimationType.Attack:
                    {
                        SetBoolAnimation(ATTACK_NAME, true);
                        SetBoolAnimation(IDLE_NAME, false);
                        SetBoolAnimation(WALK_NAME, false);
                        SetBoolAnimation(DEATH_NAME, false);
                        break;
                    }
                case AnimationType.Death:
                    {

                        SetBoolAnimation(DEATH_NAME, true);
                        SetBoolAnimation(IDLE_NAME, false);
                        SetBoolAnimation(WALK_NAME, false);
                        SetBoolAnimation(ATTACK_NAME, false);
                        break;
                    }
            }
        }

        private void SetBoolAnimation(string name, bool value)
        {
            _animator.SetBool(name, value);
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
