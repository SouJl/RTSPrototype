using UnityEngine;
using System.Collections.Generic;
using RTSPrototype.UIModel;
using RTSPrototype.UIView;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands;
using Zenject;

namespace RTSPrototype.UIPresenter.CommandsPresenter
{
    public class CommandButtonsPresenter : MonoBehaviour
    {        
        [SerializeField] private CommandButtonsView _view;

        [Inject] private IRTSValue<ISelectable> _selectable;
        [Inject] private CommandButtonsModel _model;

        private ISelectable _currentSelectable;

        private void Start()
        {
            _view.OnClick += _model.OnCommandButtonClick;
            _model.OnCommandSent += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
            _model.OnCommandAccepted += _view.BlockInteractions;

            _selectable.OnNewValue += OnSelected;
            OnSelected(_selectable.CurrentValue);
        }

        private void OnSelected(ISelectable selected)
        {
            if (_currentSelectable == selected)
            {
                return;
            }
            if (_currentSelectable != null)
            {
                _model.OnSelectionChanged();
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
    }
}
