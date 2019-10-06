using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    public class AgentInventory : MonoBehaviour
    {
        private List<InventoryItem> _items;

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
                var max = _items.Max(x => x.dmg);
                var maxWeapon = _items.First(x => x.dmg == max);
                return maxWeapon;
            }
        }

        public int Damage => EquipedItem.dmg;
    }
}
