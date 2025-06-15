using GameFishing;
using UnityEngine;

namespace GameRoot
{
    public class FishingRoot : CompositeRoot
    {
        [SerializeField] private FishingManager _manager;
        [SerializeField] private KittenHand _hand;

        internal override void Init()
        {
            _manager.Init(GlobalServices.SceneLoader);
            _hand.Init(GlobalServices.Input);
        }
    }
}