using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using RTSPrototype.Abstractions;
using System.Collections.Generic;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;

namespace RTSPrototype.UIView 
{
    public class CommandButtonsView : MonoBehaviour
    {
        public Action<ICommandExecutor, ICommandsQueue> OnClick;

        [Header("Units Buttons")]
        [SerializeField] private GameObject _moveButton;
        [SerializeField] private GameObject _attackButton;
        [SerializeField] private GameObject _patrolButton;
        [SerializeField] private GameObject _stopButton;

        [Space(10)]
        [Header("Building Buttons")]
        [SerializeField] private GameObject _produceUnitButton;
        [SerializeField] private GameObject _setRallyPointButton;

        private Dictionary<Type, GameObject> _buttonsDictionary;
        private void Start()
        {
            _buttonsDictionary = new Dictionary<Type, GameObject>();

            _buttonsDictionary.Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);
            _buttonsDictionary.Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);
            _buttonsDictionary.Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);
            _buttonsDictionary.Add(typeof(CommandExecutorBase<IStopCommand>), _stopButton);

            _buttonsDictionary.Add(typeof(CommandExecutorBase<IProduceUnitCommand>),_produceUnitButton);
            _buttonsDictionary.Add(typeof(CommandExecutorBase<ISetRallyPointCommand>), _setRallyPointButton);
        }

        public void BlockInteractions(ICommandExecutor ce)
        {
            UnblockAllInteractions();
            getButtonGameObjectByType(ce.GetType())
                .GetComponent<Selectable>().interactable = false;
        }

        public void UnblockAllInteractions() => SetInteractible(true);
        
        private void SetInteractible(bool value)
        {
            _attackButton.GetComponent<Selectable>().interactable = value;
            _moveButton.GetComponent<Selectable>().interactable = value;
            _patrolButton.GetComponent<Selectable>().interactable = value;
            _stopButton.GetComponent<Selectable>().interactable = value;
            _produceUnitButton.GetComponent<Selectable>().interactable = value;
            _setRallyPointButton.GetComponent<Selectable>().interactable = value;
        }

        public void MakeLayout(IList<ICommandExecutor> commandExecutors, ICommandsQueue queue)
        {
            for (int i = 0; i < commandExecutors.Count; i++) 
            {
                ICommandExecutor currentExecutor = commandExecutors[i];
                
                if (currentExecutor == null) 
                    continue;
                
                var buttonGameObject = _buttonsDictionary
                  .Where(type => type.Key.IsAssignableFrom(currentExecutor.GetType()))
                  .First().Value;

                buttonGameObject.SetActive(true);
                var button = buttonGameObject.GetComponent<Button>();
                button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor, queue));
            }
        }

        public void Clear()
        {
            foreach (var element in _buttonsDictionary)
            {
                element.Value.GetComponent<Button>().onClick.RemoveAllListeners();
                element.Value.SetActive(false);
            }
        }

        private GameObject getButtonGameObjectByType(Type executorInstanceType)
        {
            return _buttonsDictionary
            .Where(type => type.Key.IsAssignableFrom(executorInstanceType))
            .First()
            .Value;
        }
    }
}

