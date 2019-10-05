using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    [CreateAssetMenu]
    public class InventoryItem : ScriptableObject
    {
        public readonly float baseBidPrice;

        public readonly string Name;

        public readonly int dmg;

        public InventoryItem(float baseBidPrice, string name, int dmg)
        {
            this.baseBidPrice = baseBidPrice;
            Name = name;
            this.dmg = dmg;
        }

        public InventoryItem(InventoryItem item) : this(item.baseBidPrice, item.Name, item.dmg)
        {
        
        }
    }
}
