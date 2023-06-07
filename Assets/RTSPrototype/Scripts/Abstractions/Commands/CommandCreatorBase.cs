using System;
using RTSPrototype.Abstractions.Commands;

namespace RTSPrototype.Abstractions
{
    public abstract class CommandCreatorBase<T> where T : class, ICommand
    {
        public ICommandExecutor ProcessCommandExecutor(
            ICommandExecutor commandExecutor, 
            Action<T> callback)
        {
            var classSpecificExecutor = commandExecutor as CommandExecutorBase<T>;
            if (classSpecificExecutor != null)
            {
                classSpecificCommandCreation(callback);
            }
            return commandExecutor;
        }

        protected abstract void classSpecificCommandCreation(Action<T> creationCallback);
        public virtual void ProcessCancel() { }
    }
}
