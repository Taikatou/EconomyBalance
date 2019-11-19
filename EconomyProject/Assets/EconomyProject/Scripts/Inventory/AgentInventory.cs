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
            ResetInventory();
            Items = new List<InventoryItem>();
        }

        public void AddItem(InventoryItem item)
        {
            Items.Add(item);
            Refresh();
        }

        public void ResetInventory()
        {
            if (Items == null)
            {
                Items = new List<InventoryItem>();
            }
            else
            {
                Items?.Clear();
            }

            foreach (var item in startInventory)
            {
                InventoryItem generatedItem = ScriptableObject.CreateInstance("InventoryItem") as InventoryItem;
                if (generatedItem != null)
                {
                    generatedItem.Init(item);

                    Items?.Add(generatedItem);
                }
            }

            Refresh();
        }
    }
}
