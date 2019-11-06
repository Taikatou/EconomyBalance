using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    [System.Serializable]
    public struct ShopItem
    {
        public int price;

        public int number;

        public ShopItem(int price, int number)
        {
            this.price = price;
            this.number = number;
        }

        public ShopItem(ShopItem item)
        {
            this.price = item.price;
            this.number = item.number;
        }

        public void ChangePrice(int newPrice)
        {
            price = newPrice;
        }
    }

    public class ShopAbility : MonoBehaviour
    {
        public List<InventoryItem> shopItems;

        public ShopItem startItem;

        private Dictionary<InventoryItem, ShopItem> _itemPrices;

        public int startNumber = 10;

        private void Start()
        {
            _itemPrices = new Dictionary<InventoryItem, ShopItem>();
            foreach (var shopItem in shopItems)
            {
                _itemPrices.Add(shopItem, startItem);
            }
        }

        public void ChangePrice(InventoryItem item, int price)
        {
            var hasItem = _itemPrices.ContainsKey(item);
            if (hasItem)
            {
                _itemPrices[item].ChangePrice(price);
            }
        }

        public void AddItem(InventoryItem item, ShopItem shopInv)
        {
            var hasItem = _itemPrices.ContainsKey(item);
            if (!hasItem)
            {
                _itemPrices.Add(item, shopInv);
            }
        }
    }
}
