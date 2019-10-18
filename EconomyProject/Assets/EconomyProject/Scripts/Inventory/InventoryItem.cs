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

        public void Init(InventoryItem item)
        {
            baseBidPrice = item.baseBidPrice;
            Name = item.Name;
            baseDurability = item.baseDurability;
            efficiency = item.efficiency;
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
