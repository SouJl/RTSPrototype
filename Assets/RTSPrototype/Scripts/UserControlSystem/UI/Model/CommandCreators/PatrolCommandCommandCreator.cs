﻿using System;
using RTSPrototype.Abstractions;
using RTSPrototype.Abstractions.Commands.CommandInterfaces;
using RTSPrototype.UIModel.CommandRealization;
using RTSPrototype.Utils;
using Zenject;

namespace RTSPrototype.UIModel.CommandCreators
{
    public class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void classSpecificCommandCreation(Action<IPatrolCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new PatrolCommand()));
        }
    }
}
