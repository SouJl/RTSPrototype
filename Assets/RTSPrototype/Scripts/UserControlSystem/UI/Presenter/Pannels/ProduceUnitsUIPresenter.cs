using UniRx;
using System;
using Zenject;
using UnityEngine;
using RTSPrototype.UIView;
using RTSPrototype.UIModel;

namespace RTSPrototype.UIPresenter
{
    public class ProduceUnitsUIPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _uiHolder;

        private IDisposable _productionQueueAddCt;
        private IDisposable _productionQueueRemoveCt;
        private IDisposable _productionQueueReplaceCt;
        private IDisposable _cancelButtonCts;

        [Inject]
        private void Init(ProduceUnitsUIModel model, ProduceUnitsUIView view)
        {
            model.UnitProducers.Subscribe(unitProducer =>
            {
                _productionQueueAddCt?.Dispose();
                _productionQueueRemoveCt?.Dispose();
                _productionQueueReplaceCt?.Dispose();
                _cancelButtonCts?.Dispose();

                view.Clear();
                _uiHolder.SetActive(unitProducer != null);
                if (unitProducer != null)
                {
                    _productionQueueAddCt = unitProducer.Queue
                    .ObserveAdd()
                    .Subscribe(addEvent => view.AddTask(addEvent.Value, addEvent.Index));

                    _productionQueueRemoveCt = unitProducer.Queue
                    .ObserveRemove()
                    .Subscribe(removeEvent => view.RemoveTask(removeEvent.Index));

                    _productionQueueReplaceCt = unitProducer.Queue
                    .ObserveReplace()
                    .Subscribe(replaceEvent => view.ReplaceTask(replaceEvent.NewValue, replaceEvent.Index));
                    
                    _cancelButtonCts = view.CancelButtonClicks.Subscribe(unitProducer.Cancel);

                    for (int i = 0; i < unitProducer.Queue.Count; i++)
                    {
                        view.AddTask(unitProducer.Queue[i], i);
                    }
                }
            });
        }

    }
}
