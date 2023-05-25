using System;
using UnityEngine;
using System.Collections.Generic;
using RTSPrototype.UIModel;
using RTSPrototype.UIView;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Utils;

namespace RTSPrototype.UIPresenter.CommandsPresenter
{
    public class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private SelectedValue _selectable;
        [SerializeField] private AssetsContext _context;
        [SerializeField] private CommandButtonsView _view;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectable.OnSelected += OnSelected;
            OnSelected(_selectable.CurrentValue);

            _view.OnClick += OnButtonClick;
        }

        private void OnSelected(ISelectable selected)
        {
            if (_currentSelectable == selected)
            {
                return;
            }
            _currentSelectable = selected;

            _view.Clear();

            if (selected != null)
            {
                var commandExecutors = new List<ICommandExecutor>();

                commandExecutors
                    .AddRange((selected as Component)
                    .GetComponentsInParent<ICommandExecutor>());

                _view.MakeLayout(commandExecutors);
            }
        }

        private void OnButtonClick(ICommandExecutor commandExecutor)
        {
            var unitProducer = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
            if (unitProducer != null)
            {
                unitProducer.ExecuteCommand(_context.Inject(new ProduceUnitCommand()));
                return;
            }
            throw new ApplicationException($"{nameof(CommandButtonsPresenter)}.{nameof(OnButtonClick)}:" +
                $" Unknown type of commands executor: { commandExecutor.GetType().FullName }!");
        }
    }
}
