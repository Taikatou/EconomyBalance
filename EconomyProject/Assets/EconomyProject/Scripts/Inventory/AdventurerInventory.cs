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
                if (items == null)
                {
                    ResetInventory();
                }
                if (items.Count > 0)
                {
                    var max = items.Max(x => x.efficiency);
                    var maxWeapon = items.First(x => Math.Abs(x.efficiency - max) < 0.01);
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
                items.Remove(EquipedItem);
            }
        }
    }
}
