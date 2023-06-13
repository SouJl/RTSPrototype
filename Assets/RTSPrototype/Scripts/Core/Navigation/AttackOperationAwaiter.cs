
using RTSPrototype.Utils;

namespace RTSPrototype.Core.Navigation
{
    public class AttackOperationAwaiter : AwaiterBase<AsyncExtensions.Void> 
    {
        private AttackOperation _attackOperation;
        public AttackOperationAwaiter(AttackOperation attackOperation)
        {
            _attackOperation = attackOperation;
            attackOperation.OnComplete += OnComplete;
        }

        private void OnComplete()
        {
            _attackOperation.OnComplete -= OnComplete;
            OnWaitFinish(new AsyncExtensions.Void());
        }

    }
}
