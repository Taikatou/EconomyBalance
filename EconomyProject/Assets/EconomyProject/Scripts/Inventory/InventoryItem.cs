using UnityEngine;

namespace Assets.EconomyProject.Scripts.Inventory
{
    [CreateAssetMenu]
    public class InventoryItem : ScriptableObject
    {
        public float baseBidPrice;

        public string Name;

        public int dmg;

        public void Init(float baseBidPrice, string name, int dmg)
        {
            this.baseBidPrice = baseBidPrice;
            Name = name;
            this.dmg = dmg;
        }

        public void Init(InventoryItem item)
        {
            Init(item.baseBidPrice, item.Name, item.dmg);
        }
    }
}
