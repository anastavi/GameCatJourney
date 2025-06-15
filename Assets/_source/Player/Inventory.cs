using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlayer
{
    public class Inventory
    {
        [SerializeField] private InventoryView _view;

        private readonly List<Items> _items;

        public Inventory(InventoryView view)
        {
            _items = new();
            _view = view;
            Load();
            UpdateView();
        }

        public void AddItem(Items item)
        {
            if (_items.Contains(item))
                return;

            _items.Add(item);
            UpdateView();
            Save();
        }

        public bool RemoveItem(Items item)
        {
            if (_items.Contains(item) == false)
                return false;

            _items.Remove(item);
            UpdateView();
            PlayerPrefs.SetInt(item.ToString(), 0);
            PlayerPrefs.Save();
            return true;
        }

        private void UpdateView()
        {
            _view.UpdateView(_items);
        }

        private void Save()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                PlayerPrefs.SetInt(_items[i].ToString(), 1);
                PlayerPrefs.Save();
            }
        }

        private void Load()
        {
            _items.Clear();

            foreach (Items item in Enum.GetValues(typeof(Items)))
            {
                var has = PlayerPrefs.GetInt(item.ToString(), 0);

                if (has == 1)
                    _items.Add(item);
            }
        }
    }
}