using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    public class AgentInventory : MonoBehaviour
    {
        private List<InventoryItem> _items;

        public int ItemCount => _items.Count;

        public List<InventoryItem> startInventory;
        void Start()
        {
            _items = new List<InventoryItem>();
            foreach(var item in startInventory)
            {
                _items.Add(item);
            }
        }

        public void AddItem(InventoryItem item)
        {
            _items.Add(item);
        }

        public InventoryItem EquipedItem
        {
            get
            {
                if (_items.Count > 0)
                {
                    var max = _items.Max(x => x.efficiency);
                    var maxWeapon = _items.First(x => Math.Abs(x.efficiency - max) < 0.01);
                    return maxWeapon;
                }
                return null;
            }
        }

        public void DecreaseDurability()
        {
            if(EquipedItem)
            {
                EquipedItem.DecreaseDurability();
                if (EquipedItem.Broken)
                {
                    _items.Remove(EquipedItem);
                }
            }
        }
    }
}
