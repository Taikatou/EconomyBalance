using System;
using System.Collections.Generic;
using System.Linq;
using Assets.EconomyProject.Scripts.UI.ShopUI;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public class MarketPlace : MonoBehaviour
    {
        private Dictionary<ShopItem, IAdventurerScroll> _sellers;

        public List<ShopItem> ItemList => _sellers.Keys.ToList();

        public DateTime LastUpdated { get; set; }

        private void Start()
        {
            _sellers = new Dictionary<ShopItem, IAdventurerScroll>();
        }

        public IAdventurerScroll GetSeller(ShopItem item)
        {
            var contains = _sellers.ContainsKey(item);
            if (contains)
            {
                return _sellers[item];
            }

            return null;
        }

        public bool SellerHasItem(ShopItem toCompare, IAdventurerScroll seller)
        {
            foreach (var item in ItemList)
            {
                if (item.inventoryItem.itemName == toCompare.inventoryItem.itemName &&
                    item.price == toCompare.price && _sellers[item] == seller)
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveItem(ShopItem item, int number=1)
        {
            var toRemove = item.DeductStock(number);
            if (toRemove)
            {
                _sellers.Remove(item);
            }
            LastUpdated = DateTime.Now;
        }

        public void TryTransferItemToOtherShop(ShopItem item, AgentShopScrollList otherShop, ShopScrollList thisShop)
        {
            var seller = GetSeller(item);

            var canBuy = otherShop.Gold >= item.price;
            var sellerValid = seller != null;

            if (canBuy && sellerValid)
            {
                seller.Wallet.EarnMoney(item.price);
                otherShop.Gold -= item.price;

                otherShop.AddItem(item);
                thisShop.RemoveItem(item, 1);

                thisShop.RefreshDisplay();
                otherShop.RefreshDisplay();
                Debug.Log("enough gold");
            }
            Debug.Log("attempted");
        }

        public void AddItem(ShopItem item, IAdventurerScroll shopAgent)
        {
            LastUpdated = DateTime.Now;
            foreach (var i in ItemList)
            {
                var isSeller = SellerHasItem(item, shopAgent);

                var isItem = i.inventoryItem == item.inventoryItem;
                var isPrice = item.price == i.price;
                //Debug.Log(isSeller + "\t" + isItem + "\t" + isPrice);
                if (isSeller && isItem && isPrice)
                {
                    i.IncreaseStock(item.stock);
                    return;
                }
            }
            ItemList.Add(item);
            var containsItem = _sellers.ContainsKey(item);
            if (!containsItem)
            {
                _sellers.Add(item, shopAgent);
            }
        }
    }
}
