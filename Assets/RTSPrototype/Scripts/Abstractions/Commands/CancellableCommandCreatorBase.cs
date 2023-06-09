﻿using System;
using Zenject;
using System.Threading;
using RTSPrototype.Utils;
using RTSPrototype.Abstractions.Commands;
using RTSPrototype.Abstractions.AssetsInjector;
using RTSPrototype.Abstractions.ScriptableObjects;

namespace RTSPrototype.Abstractions
{
    public abstract class CancellableCommandCreatorBase<TCommand, TArgument> : CommandCreatorBase<TCommand> where TCommand : class, ICommand
    {
        [Inject] private IAssetContext _context;
        [Inject] private IAwaitable<TArgument> _awaitableArgument;

        private CancellationTokenSource _ctSource;
        
        protected override sealed async void classSpecificCommandCreation(Action<TCommand> creationCallback)
        {
            _ctSource = new CancellationTokenSource();
            try
            {
                var argument = await _awaitableArgument.RunWithCancellation(_ctSource.Token);
                creationCallback?.Invoke(_context.Inject(CreateCommand(argument)));
            }
            catch 
            {

            }
        }

        protected abstract TCommand CreateCommand(TArgument argument);

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            if (_ctSource != null)
            {
                _ctSource.Cancel();
                _ctSource.Dispose();
                _ctSource = null;
            }
        }
    }
}
