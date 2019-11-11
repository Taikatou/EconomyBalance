using System;
using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using Assets.EconomyProject.Scripts.UI.ShopUI.Buttons;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists
{

    public interface IScrollList <T>
    {
        void SelectItem(T item, int number = 1);
    }
    public abstract class AbstractScrollList<T, TQ> : MonoBehaviour, IScrollList<T>  where TQ : SampleButton<T>
    {
        public abstract List<T> ItemList { get; }
        public Transform contentPanel;
        public SimpleObjectPool buttonObjectPool;

        public MarketPlace marketPlace;


        private DateTime _lastUpdated;

        public abstract void SelectItem(T item, int number=1);

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

                    var sampleButton = newButton.GetComponent<TQ>();
                    sampleButton.Setup(item, this);
                }
            }
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