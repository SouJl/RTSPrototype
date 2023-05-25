using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using System.Collections.Generic;

namespace RTSPrototype.UIView 
{
    public class CommandButtonsView : MonoBehaviour
    {
        public event Action<ICommandExecutor> OnClick;

        [Header("Action Buttons")]
        [SerializeField] private GameObject _moveButton;
        [SerializeField] private GameObject _attackButton;
        [SerializeField] private GameObject _patrolButton;
        [SerializeField] private GameObject _stopButton;
        [Header("Create Buttons")]
        [SerializeField] private GameObject _produceUnitButton;

        private Dictionary<Type, GameObject> _buttonsDictionary;
        private void Start()
        {
            _buttonsDictionary = new Dictionary<Type, GameObject>();

            _buttonsDictionary.Add(typeof(CommandExecutorBase<IAttackCommand>), _attackButton);
            _buttonsDictionary.Add(typeof(CommandExecutorBase<IMoveCommand>), _moveButton);
            _buttonsDictionary.Add(typeof(CommandExecutorBase<IPatrolCommand>), _patrolButton);
            _buttonsDictionary.Add(typeof(CommandExecutorBase<IStopCommand>), _stopButton);
            _buttonsDictionary.Add(typeof(CommandExecutorBase<IProduceUnitCommand>),_produceUnitButton);
        }


        public void MakeLayout(IList<ICommandExecutor> commandExecutors)
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
                button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor));
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

    }
}

