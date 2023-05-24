using UnityEngine;

namespace RTSPrototype.Abstractions.Commands
{
    public abstract class CommandExecutorBase<T> : MonoBehaviour, ICommandExecutor where T : ICommand
    {
        public void ExecuteCommand(object command) => ExcecuteSpecifiedCommand((T)command);

        public abstract void ExcecuteSpecifiedCommand(T command);
    }
}
