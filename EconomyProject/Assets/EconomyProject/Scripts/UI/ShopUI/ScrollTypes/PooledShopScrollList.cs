﻿using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes
{
    public class PooledShopScrollList : ShopScrollList
    {
        // Start is called before the first frame update
        public override List<ShopItem> ItemList => marketPlace.ItemList;

        public AgentShopScrollList otherShop;

        public MarketPlace marketPlace;

        public void AddItem(ShopItem item, IAdventurerScroll shopAgent)
        {
            foreach (var i in ItemList)
            {
                var isSeller = marketPlace.GetSeller(item) == shopAgent;
                var isItem = i.inventoryItem == item.inventoryItem;
                var isPrice = item.price == i.price;
                Debug.Log(isSeller + "\t" + isItem + "\t" + isPrice);
                if (isSeller && isItem && isPrice)
                {
                    i.IncreaseStock(item.stock);
                    return;
                }
            }
            marketPlace.AddItem(item, shopAgent);
        }

        public override void TryTransferItemToOtherShop(ShopItem item)
        {
            var seller = marketPlace.GetSeller(item);

            var canBuy = otherShop.Gold >= item.price;
            var sellerValid = seller != null;

            if (canBuy && sellerValid)
            {
                seller.Wallet.EarnMoney(item.price);
                otherShop.Gold -= item.price;

                AddItem(item, otherShop);
                RemoveItem(item, 1);

                RefreshDisplay();
                otherShop.RefreshDisplay();
                Debug.Log("enough gold");
            }
            Debug.Log("attempted");
        }

        protected override void RemoveItem(ShopItem itemToRemove, int number)
        {
            var toRemove = itemToRemove.DeductStock(number);
            if (toRemove)
            {
                marketPlace.RemoveItem(itemToRemove);
            }
        }
    }
}
