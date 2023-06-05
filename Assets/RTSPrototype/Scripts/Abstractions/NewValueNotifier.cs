using System;
using RTSPrototype.Utils;

namespace RTSPrototype.Abstractions
{
    public class NewValueNotifier<TAwaited> : IAwaiter<TAwaited>
    {
        private readonly RTSValueBase<TAwaited> _valueBase;
        private TAwaited _result;
        private Action _continuation;
        private bool _isCompleted;
        public NewValueNotifier(RTSValueBase<TAwaited> valueBase)
        {
            _valueBase = valueBase;
            _valueBase.OnNewValue += onNewValue;
        }
        private void onNewValue(TAwaited obj)
        {
            _valueBase.OnNewValue -= onNewValue;
            _result = obj;
            _isCompleted = true;
            _continuation?.Invoke();
        }

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

        public bool IsCompleted => _isCompleted;
        public TAwaited GetResult() => _result;
    }
}
