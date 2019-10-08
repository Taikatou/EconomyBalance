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

        [HideInInspector]
        public bool Broken;

        public void Awake()
        {
            Durability = BaseDurability;
        }

        public void Init(float baseBidPrice, string name, int dmg, int durability)
        {
            this.baseBidPrice = baseBidPrice;
            this.Name = name;
            this.damage = dmg;
            this.BaseDurability = durability;
        }

        public void Init(InventoryItem item)
        {
            Init(item.baseBidPrice, item.Name, item.damage, item.BaseDurability);
        }

        public void DecreaseDurability()
        {
            Durability--;
            if (Durability <= 0)
            {
                Broken = true;
            }
        }
    }
}
