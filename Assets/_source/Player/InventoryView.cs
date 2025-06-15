using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlayer
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private AllItemsList _allItemsList;
        [SerializeField] private Image[] _itemsSlots;

        public void UpdateView(List<Items> items)
        {
            for (int i = 0; i < _itemsSlots.Length; i++)
            {
                _itemsSlots[i].sprite = null;
                _itemsSlots[i].enabled = false;
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (i >= _itemsSlots.Length)
                    return;

                _itemsSlots[i].sprite = _allItemsList.TryGetItemSprite(items[i]);
                _itemsSlots[i].preserveAspect = true;
                _itemsSlots[i].enabled = true;
            }
        }
    }
}