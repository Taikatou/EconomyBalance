﻿using System.Collections.Generic;
using System.Linq;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public class MarketPlace : MonoBehaviour
    {
        private Dictionary<ShopItem, IAdventurerScroll> _sellers;

        public List<ShopItem> ItemList => _sellers.Keys.ToList();

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

        private void Start()
        {
            _sellers = new Dictionary<ShopItem, IAdventurerScroll>();
        }

        public void AddItem(ShopItem item, IAdventurerScroll seller)
        {
            ItemList.Add(item);
            var containsItem = _sellers.ContainsKey(item);
            if (!containsItem)
            {
                _sellers.Add(item, seller);
            }
        }

        public void RemoveItem(ShopItem item)
        {
            _sellers.Remove(item);
        }
    }
}
