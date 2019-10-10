using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class GameAuction : EconomySystem
    {
        public int ItemCount => _inventoryItems.Count;

        public float bidIncrement = 5.0f;

        [HideInInspector]
        public InventoryItem auctionedItem;

        [HideInInspector]
        public float currentItemPrice;

        [HideInInspector]
        public EconomyAgent currentHighestBidder;

        [HideInInspector]
        public float currentAuctionTime;

        private float _auctionTime = 5.0f;

        private bool _bidOn;

        private bool _bidLast;

        private bool _auctionOn;

        private List<InventoryItem> _inventoryItems;

        public float Progress => currentAuctionTime / _auctionTime;

        private DataLogger Logger => GetComponent<DataLogger>();

        public void SetAuctionItem()
        {
            _auctionOn = _inventoryItems.Count > 0;
            if (_auctionOn)
            {
                System.Random rnd = new System.Random();

                int index = rnd.Next(_inventoryItems.Count);

                auctionedItem = _inventoryItems[index];

                currentAuctionTime = 0.0f;

                currentItemPrice = auctionedItem.baseBidPrice;

                currentHighestBidder = null;

                _bidOn = false;
                _bidLast = false;
            }
        }

        public void AddAuctionItem(InventoryItem item)
        {
            _inventoryItems.Add(item);
        }

        private void Start()
        {
            _inventoryItems = new List<InventoryItem>();

            actionChoice = AgentScreen.Auction;
        }

        // Returns if the auction is over
        private void Update()
        {
            if (ItemCount > 0 && CurrentPlayers.Length > 0)
            {
                if (!_auctionOn)
                {
                    SetAuctionItem();
                }
                currentAuctionTime += Time.deltaTime;
                if (currentAuctionTime >= _auctionTime)
                {
                    currentAuctionTime = 0.0f;
                    if (!_bidOn)
                    {
                        _inventoryItems.Remove(auctionedItem);
                        if(_bidLast)
                        {
                            currentHighestBidder.BoughtItem(auctionedItem, currentItemPrice);
                            Logger.AddAuctionItem(auctionedItem, currentItemPrice);
                        }
                        SetAuctionItem();
                    }
                    else
                    {
                        _bidOn = false;
                    }
                }
            }
        }

        public void Bid(EconomyAgent player)
        {
            if(player != currentHighestBidder)
            {
                float newPrice = currentItemPrice + bidIncrement;
                if (player.Money >= newPrice)
                {
                    currentHighestBidder = player;
                    currentItemPrice = newPrice;
                    _bidOn = true;
                    _bidLast = true;
                }
            }
        }
    }
}
