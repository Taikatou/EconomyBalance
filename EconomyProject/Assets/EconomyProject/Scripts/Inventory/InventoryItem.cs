using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    [CreateAssetMenu]
    public class InventoryItem : ScriptableObject
    {
        public float baseBidPrice;

        public string Name;

        public int damage;

        public int BaseDurability;

        [HideInInspector]
        public int Durability;

        public bool UnBreakable = false;

        public bool Broken => !UnBreakable && Durability == 0;

        private void OnEnable()
        {
            Durability = BaseDurability;
        }

        public void Init(float basePrice, string name, int dmg, int durability)
        {
            baseBidPrice = basePrice;
            Name = name;
            damage = dmg;
            BaseDurability = durability;
            Durability = durability;
        }

        public void Init(InventoryItem item)
        {
            Init(item.baseBidPrice, item.Name, item.damage, item.BaseDurability);
        }

        public void DecreaseDurability()
        {
            if(!UnBreakable)
            {
                Durability--;
            }
        }
    }
}
