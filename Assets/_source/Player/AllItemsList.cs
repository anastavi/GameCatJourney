using System;
using UnityEngine;

namespace GamePlayer
{
    [CreateAssetMenu(fileName = nameof(AllItemsList), menuName = "Items/" + nameof(AllItemsList))]
    public class AllItemsList : ScriptableObject
    {
        public ItemView[] _items;

        public Sprite TryGetItemSprite(Items item)
        {
            for (int i = 0; i < _items.Length; i++)
                if (item == _items[i].Name)
                    return _items[i].Sprite;

            return null;
        }
    }

    [Serializable]
    public class ItemView
    {
        public Items Name;
        public Sprite Sprite;
    }
}