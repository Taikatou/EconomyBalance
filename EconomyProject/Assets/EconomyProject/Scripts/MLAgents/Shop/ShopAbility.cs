using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    [System.Serializable]
    public struct ShopItem
    {
        public InventoryItem item;

        public int itemPrice;
    }

    public class ShopAbility : MonoBehaviour
    {
        public List<ShopItem> shopItems;

        private Dictionary<InventoryItem, int> _itemPrices;

        void Start()
        {
            _itemPrices = new Dictionary<InventoryItem, int>();
            foreach (var shopItem in shopItems)
            {
                _itemPrices.Add(shopItem.item, shopItem.itemPrice);
            }
        }

        public void ChangePrice(InventoryItem item, int price)
        {
            var hasItem = _itemPrices.ContainsKey(item);
            if (hasItem)
            {
                _itemPrices[item] = price;
            }
        }

        public void AddItem(InventoryItem item, int price)
        {
            var hasItem = _itemPrices.ContainsKey(item);
            if (!hasItem)
            {
                _itemPrices.Add(item, price);
                //shopItems.Add();
            }
        }

        public void UpdatePrice(InventoryItem item, int priceChange)
        {
            if (_itemPrices.ContainsKey(item))
            {
                _itemPrices[item] += priceChange;
            }
        }
    }
}
