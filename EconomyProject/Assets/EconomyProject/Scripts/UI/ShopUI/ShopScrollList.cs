using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.ShopUI
{
    [System.Serializable]
    public class Item
    {
        public string itemName;
        public Sprite icon;
        public float price = 1;
    }

    public abstract class ShopScrollList : MonoBehaviour
    {
        public abstract List<Item> ItemList { get; set; }
        public Transform contentPanel;
        public ShopScrollList otherShop;
        public Text myGoldDisplay;
        public SimpleObjectPool buttonObjectPool;

        public abstract double Gold { get; set; }


        // Use this for initialization
        void Start()
        {
            RefreshDisplay();
        }

        public void RefreshDisplay()
        {
            myGoldDisplay.text = "Gold: " + Gold.ToString(CultureInfo.InvariantCulture);
            RemoveButtons();
            AddButtons();
        }

        private void RemoveButtons()
        {
            while (contentPanel.childCount > 0)
            {
                GameObject toRemove = transform.GetChild(0).gameObject;
                buttonObjectPool.ReturnObject(toRemove);
            }
        }

        protected void AddButtons()
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                Item item = ItemList[i];
                GameObject newButton = buttonObjectPool.GetObject();
                newButton.transform.SetParent(contentPanel);

                SampleButton sampleButton = newButton.GetComponent<SampleButton>();
                sampleButton.Setup(item, this);
            }
        }

        public void TryTransferItemToOtherShop(Item item)
        {
            if (otherShop.Gold >= item.price)
            {
                Gold += item.price;
                otherShop.Gold -= item.price;

                AddItem(item, otherShop);
                RemoveItem(item, this);

                RefreshDisplay();
                otherShop.RefreshDisplay();
                Debug.Log("enough gold");

            }

            Debug.Log("attempted");
        }

        void AddItem(Item itemToAdd, ShopScrollList shopList)
        {
            shopList.ItemList.Add(itemToAdd);
        }

        private void RemoveItem(Item itemToRemove, ShopScrollList shopList)
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