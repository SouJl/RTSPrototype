using RTSPrototype.Utils;

namespace RTSPrototype.Core.Navigation
{
    public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
    {
        private readonly UnitMovementStop _unitMovementStop;

        public StopAwaiter(UnitMovementStop unitMovementStop)
        {
            _unitMovementStop = unitMovementStop;
            _unitMovementStop.OnStop += OnStop;
        }

        private void OnStop()
        {
            _unitMovementStop.OnStop -= OnStop;
            OnWaitFinish(new AsyncExtensions.Void());
        }
    }

}
