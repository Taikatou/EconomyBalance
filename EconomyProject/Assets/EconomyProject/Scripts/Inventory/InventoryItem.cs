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

        public bool Broken => Durability == 0;

        private void OnEnable()
        {
            Durability = BaseDurability;
        }

        public void Init(float baseBidPrice, string name, int dmg, int durability)
        {
            this.baseBidPrice = baseBidPrice;
            this.Name = name;
            this.damage = dmg;
            this.BaseDurability = durability;
            this.Durability = durability;
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
