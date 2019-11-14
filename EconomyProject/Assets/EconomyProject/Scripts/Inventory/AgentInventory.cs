using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    public class AgentInventory : MonoBehaviour
    {
        protected List<InventoryItem> items;

        public List<InventoryItem> startInventory;

        public int ItemCount => items.Count;

        private void Start()
        {
            ResetInventory();
            items = new List<InventoryItem>();
        }

        public void AddItem(InventoryItem item)
        {
            items.Add(item);
        }

        public void ResetInventory()
        {
            if (items == null)
            {
                items = new List<InventoryItem>();
            }
            else
            {
                items?.Clear();
            }

            foreach (var item in startInventory)
            {
                InventoryItem generatedItem = ScriptableObject.CreateInstance("InventoryItem") as InventoryItem;
                if (generatedItem != null)
                {
                    generatedItem.Init(item);

                    items?.Add(generatedItem);
                }
            }
        }
    }
}
