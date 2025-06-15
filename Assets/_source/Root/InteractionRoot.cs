using UnityEngine;
using GameInteraction;
using GameContracts;

namespace GameRoot
{
    public class InteractionRoot : CompositeRoot
    {
        [SerializeField] private InteractionTrigger _interactionFirst;
        [SerializeField] private InteractionTrigger _interactionSecond;
        [SerializeField] private InteractionTriggerView _interactionView;

        internal override void Init()
        {

        }

        private void Update()
        {
            _interactionFirst.Tick();
            _interactionSecond.Tick();
        }
    }

}