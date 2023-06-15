using Zenject;
using RTSPrototype.Abstractions.Commands;

namespace RTSPrototype.Core.CommandExecutors
{
    public class CommandExecutorsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var executors = gameObject.GetComponentsInChildren<ICommandExecutor>();
            foreach (var executor in executors)
            {
                var baseType = executor.GetType().BaseType;
                Container.Bind(baseType).FromInstance(executor);
            }
        }
    }
}
