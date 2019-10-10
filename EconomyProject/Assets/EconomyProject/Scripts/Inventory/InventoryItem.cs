using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    public enum RarityTypes { Common, UnCommon, Rare, Epic }

    [CreateAssetMenu]
    public class InventoryItem : ScriptableObject
    {
        public string Name;

        public float baseBidPrice;

        public int damage;

        public int baseDurability;

        [HideInInspector]
        public int durability;

        public bool unBreakable = false;

        public bool Broken => !unBreakable && durability == 0;

        public float efficiency;

        public RarityTypes rarityType;

        public RarityTypes maxRarityType;

        private void OnEnable()
        {
            durability = baseDurability;
        }

        public void Init(float basePrice, string name, int dmg, int durability)
        {
            baseBidPrice = basePrice;
            Name = name;
            damage = dmg;
            baseDurability = durability;
            this.durability = durability;
        }

        public void Init(InventoryItem item)
        {
            Init(item.baseBidPrice, item.Name, item.damage, item.baseDurability);
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
