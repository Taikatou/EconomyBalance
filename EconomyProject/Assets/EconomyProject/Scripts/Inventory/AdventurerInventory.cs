using System;
using System.Linq;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    public class AdventurerInventory : AgentInventory
    {
        public InventoryItem EquipedItem
        {
            get
            {
                if (Items == null)
                {
                    ResetInventory();
                }
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
            EquipedItem.DecreaseDurability();
            if (EquipedItem.Broken)
            {
                Items.Remove(EquipedItem);
            }
        }
    }
}
