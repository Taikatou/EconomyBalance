using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    public class AgentInventory : MonoBehaviour
    {
        private List<InventoryItem> _items;

        public List<InventoryItem> startInventory;

        public int ItemCount => _items.Count;

        private void Start()
        {
            ResetInventory();
        }

        public void AddItem(InventoryItem item)
        {
            _items.Add(item);
        }

        public InventoryItem EquipedItem
        {
            get
            {
                if (_items == null)
                {
                    ResetInventory();
                }
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
            EquipedItem.DecreaseDurability();
            if (EquipedItem.Broken)
            {
                _items.Remove(EquipedItem);
            }
        }

        public void ResetInventory()
        {
            if (_items == null)
            {
                _items = new List<InventoryItem>();
            }
            else
            {
                _items?.Clear();
            }

            foreach (var item in startInventory)
            {
                InventoryItem generatedItem = ScriptableObject.CreateInstance("InventoryItem") as InventoryItem;
                if (generatedItem != null)
                {
                    generatedItem.Init(item);

                    _items?.Add(generatedItem);
                }
            }
        }
    }
}
