using System.Collections.Generic;
using System.Globalization;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.ShopUI
{
    [System.Serializable]
    public class Item
    {
        public string itemName;
        public Sprite icon;
        public double price;
        public readonly int itemId;
        private static int _itemId;

        public Item(string itemName, double price)
        {
            this.itemName = itemName;
            this.price = price;
            itemId = _itemId;
            _itemId++;
        }
    }

    public abstract class ShopScrollList :  MonoBehaviour
    {
        public abstract List<Item> ItemList { get; }
        public Transform contentPanel;
        public SimpleObjectPool buttonObjectPool;


        // Use this for initialization
        void Start()
        {
            RefreshDisplay();
        }

        public virtual void RefreshDisplay()
        {
            RemoveButtons();
            AddButtons();
        }

        public void RemoveButtons()
        {
            while (contentPanel.childCount > 0)
            {
                GameObject toRemove = transform.GetChild(0).gameObject;
                buttonObjectPool.ReturnObject(toRemove);
            }
        }

        protected void AddButtons()
        {
            if (ItemList != null)
            {
                foreach (var item in ItemList)
                {
                    GameObject newButton = buttonObjectPool.GetObject();
                    newButton.transform.SetParent(contentPanel);

                    SampleButton sampleButton = newButton.GetComponent<SampleButton>();
                    sampleButton.Setup(item, this);
                }
            }
        }

        public abstract void TryTransferItemToOtherShop(Item item);

        protected void AddItem(Item itemToAdd, ShopScrollList shopList)
        {
            shopList.ItemList.Add(itemToAdd);
        }

        protected void RemoveItem(Item itemToRemove, ShopScrollList shopList)
        {
            for (int i = shopList.ItemList.Count - 1; i >= 0; i--)
            {
                if (shopList.ItemList[i] == itemToRemove)
                {
                    shopList.ItemList.RemoveAt(i);
                }
            }
        }
    }
}