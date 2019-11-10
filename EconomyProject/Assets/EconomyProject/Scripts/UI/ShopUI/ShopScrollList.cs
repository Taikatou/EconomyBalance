using System;
using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.ShopUI
{
    public abstract class ShopScrollList :  MonoBehaviour
    {
        public abstract List<ShopItem> ItemList { get; }
        public Transform contentPanel;
        public SimpleObjectPool buttonObjectPool;

        public MarketPlace marketPlace;


        private DateTime _lastUpdated;

        public abstract void TryTransferItemToOtherShop(ShopItem item);

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
                var toRemove = transform.GetChild(0).gameObject;
                buttonObjectPool.ReturnObject(toRemove);
            }
        }

        protected void AddButtons()
        {
            if (ItemList != null)
            {
                foreach (var item in ItemList)
                {
                    var newButton = buttonObjectPool.GetObject();
                    newButton.transform.SetParent(contentPanel);

                    SampleButton sampleButton = newButton.GetComponent<SampleButton>();
                    sampleButton.Setup(item, this);
                }
            }
        }

        public void TryTransferItemToOtherShop(ShopItem item, int number)
        {
            TryTransferItemToOtherShop(item);
        }

        private void Update()
        {
            if (_lastUpdated != marketPlace.LastUpdated)
            {
                _lastUpdated = marketPlace.LastUpdated;
                RefreshDisplay();
            }
        }
    }
}