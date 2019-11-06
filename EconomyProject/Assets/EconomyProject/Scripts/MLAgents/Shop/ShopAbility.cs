using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Barracuda;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    [System.Serializable]
    public struct ShopItem
    {
        public InventoryItem item;

        public int price;

        public int number;

        public ShopItem(InventoryItem item, int price, int number)
        {
            this.item = item;
            this.price = price;
            this.number = number;
        }
    }

    public class ShopAbility : MonoBehaviour
    {
        public List<ShopItem> shopItems;

        private Dictionary<InventoryItem, ShopItem> _itemPrices;

        public int startNumber = 10;

        void Start()
        {
            _itemPrices = new Dictionary<InventoryItem, ShopItem>();
            foreach (var shopItem in shopItems)
            {
                _itemPrices.Add(shopItem.item, shopItem);
            }
        }

        public void ChangePrice(InventoryItem item, int price)
        {
            var hasItem = _itemPrices.ContainsKey(item);
            if (hasItem)
            {
                var shopInv = _itemPrices[item];
                shopInv.price = price;
            }
        }

        public void AddItem(InventoryItem item, ShopItem shopInv)
        {
            var hasItem = _itemPrices.ContainsKey(item);
            if (!hasItem)
            {
                _itemPrices.Add(item, shopInv);
                shopItems.Add(shopInv);
            }
        }
    }
}
