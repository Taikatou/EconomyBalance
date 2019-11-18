using System.Collections.Generic;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    public class AgentInventory : LastUpdate
    {
        public List<InventoryItem> Items { get; private set; }

        public List<InventoryItem> startInventory;

        public int ItemCount => Items.Count;

        private void Start()
        {
            Items = new List<InventoryItem>();
            ResetInventory();
        }

        public void AddItem(InventoryItem item)
        {
            Items.Add(item);
            Refresh();
        }

        public void ResetInventory()
        {
            Items.Clear();

            foreach (var item in startInventory)
            {
                var generatedItem = ScriptableObject.CreateInstance("InventoryItem") as InventoryItem;
                if (generatedItem != null)
                {
                    generatedItem.Init(item);

                    Items.Add(generatedItem);
                }
            }

            Refresh();
        }

        public void DecreaseDurability(InventoryItem item)
        {
            if (Items.Contains(item))
            {
                item.DecreaseDurability();
                if (item.Broken)
                {
                    Items.Remove(item);
                }
            }
        }
    }
}
