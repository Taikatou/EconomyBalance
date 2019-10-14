using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    [CreateAssetMenu]
    public class InventoryItem : ScriptableObject
    {
        public string Name;

        public float baseBidPrice;

        public int baseDurability;

        [HideInInspector]
        public int durability;

        public bool unBreakable = false;

        public bool Broken => !unBreakable && durability == 0;

        public float efficiency;

        private void OnEnable()
        {
            durability = baseDurability;
        }

        public void Init(float basePrice, string name, int durability, float efficiency)
        {
            baseBidPrice = basePrice;
            Name = name;
            baseDurability = durability;
            this.durability = durability;
            this.efficiency = efficiency;
        }

        public void Init(InventoryItem item)
        {
            Init(item.baseBidPrice, item.Name, item.baseDurability, item.efficiency);
        }

        public void DecreaseDurability()
        {
            if(!unBreakable)
            {
                durability--;
            }
        }
    }
}
