using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    [CreateAssetMenu]
    public class InventoryItem : ScriptableObject
    {
        public string itemName;

        public float baseBidPrice;

        public float rewardPrice;

        public int baseDurability;

        [HideInInspector]
        public int durability;

        public bool unBreakable;

        public bool Broken => !unBreakable && durability == 0;

        public float efficiency;

        private void OnEnable()
        {
            durability = baseDurability;
        }

        public void Init(InventoryItem item)
        {
            baseBidPrice = item.baseBidPrice;
            itemName = item.itemName;
            baseDurability = item.baseDurability;
            efficiency = item.efficiency;
            unBreakable = item.unBreakable;
            durability = item.durability;
            rewardPrice = item.rewardPrice;
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
