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

        private int _currentDurability;

        private bool _toSet;

        [HideInInspector]
        public bool Broken;

        public int Durability
        {
            get
            {
                if(_toSet)
                {
                    _toSet = false;
                    _currentDurability = BaseDurability;
                }
                return _currentDurability;
            }
            set
            {
                _currentDurability = value;
            }
        }

        public void Init(float baseBidPrice, string name, int dmg, int durability)
        {
            this.baseBidPrice = baseBidPrice;
            this.Name = name;
            this.damage = dmg;
            this._currentDurability = durability;
            this.BaseDurability = durability;
        }

        public void Init(InventoryItem item)
        {
            Init(item.baseBidPrice, item.Name, item.damage, item.BaseDurability);
        }

        public void DecreaseDurability()
        {
            Durability--;
            if(Durability <= 0)
            {
                Broken = true;
            }
        }
    }
}
