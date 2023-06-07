using System;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using UnityEngine;
using Zenject;

namespace RTSPrototype.UIModel
{
    public class CommandButtonsModel
    {
        public event Action<ICommandExecutor> OnCommandAccepted;
        public event Action OnCommandSent;
        public event Action OnCommandCancel;
 
        [Inject] private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] private CommandCreatorBase<IAttackCommand> _attacker;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;
        [Inject] private CommandCreatorBase<IStopCommand> _stopper;

        [Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
        [Inject] private CommandCreatorBase<ISetRallyPointCommand> _setRallyPointProducer;

        private bool _commandIsPending;

        public void OnCommandButtonClick(ICommandExecutor commandExecutor, ICommandsQueue commandsQueue) 
        {
            if (_commandIsPending) 
            {
                ProcessOnCancel();
            }
            _commandIsPending = true;
            OnCommandAccepted?.Invoke(commandExecutor);
            
            _mover.ProcessCommandExecutor(commandExecutor, command 
                => ExecuteCommandWrapper(commandsQueue, command));

            _attacker.ProcessCommandExecutor(commandExecutor, command 
                => ExecuteCommandWrapper(commandsQueue, command));
            
            _patroller.ProcessCommandExecutor(commandExecutor, command 
                => ExecuteCommandWrapper(commandsQueue, command));

            _stopper.ProcessCommandExecutor(commandExecutor, command 
                => ExecuteCommandWrapper(commandsQueue, command));
            
            _unitProducer.ProcessCommandExecutor(commandExecutor, command 
                => ExecuteCommandWrapper(commandsQueue, command));
            
            _setRallyPointProducer.ProcessCommandExecutor(commandExecutor, command 
                => ExecuteCommandWrapper(commandsQueue, command));

        }

        public void ExecuteCommandWrapper(
            ICommandsQueue commandsQueue, 
            object command)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                commandsQueue.Clear();
            }
            commandsQueue.EnqueueCommand(command);
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
