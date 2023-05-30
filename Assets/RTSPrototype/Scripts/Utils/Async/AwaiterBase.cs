using System;

namespace RTSPrototype.Utils
{
    public abstract class AwaiterBase<T> : IAwaiter<T>
    {
        private Action _continuation;
        private bool _isCompleted;
        private T _result;

        public bool IsCompleted => _isCompleted;

        public T GetResult() => _result;

        public void OnCompleted(Action continuation) 
        {
            if (_isCompleted)
            {
                continuation?.Invoke();
            }
            else 
            {
                _continuation = continuation;
            }
        }

        protected void OnWaitFinish(T result)
        {
            _result = result;
            _isCompleted = true;
            _continuation?.Invoke();
        }

    }
}
