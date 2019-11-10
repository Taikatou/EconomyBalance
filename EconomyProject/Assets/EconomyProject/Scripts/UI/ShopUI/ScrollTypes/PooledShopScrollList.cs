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

        public override void TryTransferItemToOtherShop(ShopItem item)
        {
            marketPlace.TryTransferItemToOtherShop(item, otherShop, this);
        }

        public override void RemoveItem(ShopItem itemToRemove, int number)
        {
            var toRemove = itemToRemove.DeductStock(number);
            if (toRemove)
            {
                marketPlace.RemoveItem(itemToRemove);
            }
        }
    }
}
