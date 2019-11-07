using System.Collections.Generic;
using System.Linq;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes
{
    public class PooledShopScrollList : ShopScrollList
    {
        // Start is called before the first frame update
        public override List<Item> ItemList => _itemList;

        private readonly List<Item> _itemList = new List<Item>();

        public AgentShopScrollList otherShop;

        public void AddItem(Item item)
        {
            _itemList.Add(item);
        }

        public GameObject gameObjects;

        public IAdventurerScroll GetSeller(Item itemSale)
        {
            var sa = gameObjects.GetComponentsInChildren<ShopAgent>();
            Debug.Log(sa.Length);
            foreach (var s in sa)
            {
                foreach (var item in s.ItemInMarket)
                {
                    Debug.Log(item.itemId + "\t" +  itemSale.itemId);
                    if (item.itemId == itemSale.itemId)
                    {
                        return s;
                    }
                }
            }
            return null;
        }

        public override void TryTransferItemToOtherShop(Item item)
        {
            var seller = GetSeller(item);
            if (otherShop.Gold >= item.price)
            {
                seller.Wallet.EarnMoney(item.price);
                otherShop.Gold -= item.price;

                AddItem(item, otherShop);
                RemoveItem(item, this);

                RefreshDisplay();
                otherShop.RefreshDisplay();
                Debug.Log("enough gold");
            }
            Debug.Log("attempted");
        }
    }
}
