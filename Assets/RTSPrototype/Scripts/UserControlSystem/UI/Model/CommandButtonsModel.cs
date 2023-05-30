using System;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using Zenject;

namespace RTSPrototype.UIModel
{
    public class CommandButtonsModel
    {
        public event Action<ICommandExecutor> OnCommandAccepted;
        public event Action OnCommandSent;
        public event Action OnCommandCancel;

        [Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
        [Inject] private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;
        [Inject] private CommandCreatorBase<IStopCommand> _stopper;

        private bool _commandIsPending;

        public void OnCommandButtonClick(ICommandExecutor commandExecutor) 
        {
            if (_commandIsPending) 
            {
                ProcessOnCancel();
            }
            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProcessCommandExecutor(commandExecutor, command => 
                            ExecuteCommandWrapper(commandExecutor, command));
            
            _mover.ProcessCommandExecutor(commandExecutor, command =>
                            ExecuteCommandWrapper(commandExecutor, command));

            _attacker.ProcessCommandExecutor(commandExecutor, command =>
                            ExecuteCommandWrapper(commandExecutor, command));
            
            _patroller.ProcessCommandExecutor(commandExecutor, command => 
                            ExecuteCommandWrapper(commandExecutor, command));

            _stopper.ProcessCommandExecutor(commandExecutor, command =>
                            ExecuteCommandWrapper(commandExecutor, command));
        }

        public void ExecuteCommandWrapper(
            ICommandExecutor commandExecutor, 
            object command)
        {
            commandExecutor.ExecuteCommand(command);
            _commandIsPending = false;
            OnCommandSent?.Invoke();
        }

        public void OnSelectionChanged() 
        {
            _commandIsPending = false;
            ProcessOnCancel();

        }

        private void ProcessOnCancel()
        {
            _unitProducer.ProcessCancel();
            _mover.ProcessCancel();
            _attacker.ProcessCancel();
            _patroller.ProcessCancel();
            _stopper.ProcessCancel();

            OnCommandCancel?.Invoke();
        }
    }
}
