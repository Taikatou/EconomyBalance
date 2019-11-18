using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    public class AdventurerInventory : MonoBehaviour
    {
        public AgentInventory AgentInventory => GetComponent<AgentInventory>();
        public List<InventoryItem> Items => AgentInventory.Items;

        public InventoryItem EquipedItem
        {
            get
            {
                if (Items.Count > 0)
                {
                    var max = Items.Max(x => x.efficiency);
                    var maxWeapon = Items.First(x => Math.Abs(x.efficiency - max) < 0.01);
                    return maxWeapon;
                }
                return null;
            }
        }

        public void DecreaseDurability()
        {
            AgentInventory.DecreaseDurability(EquipedItem);
        }
    }
}
