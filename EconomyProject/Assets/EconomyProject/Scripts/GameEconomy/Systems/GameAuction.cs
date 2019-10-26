using System;
using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents;
using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems
{
    public class GameAuction : EconomySystem
    {
        public int ItemCount => _inventoryItems.Count;

        public float bidIncrement = 5.0f;

        [HideInInspector]
        public InventoryItem auctionedItem;

        [HideInInspector]
        public float currentItemPrice;

        private EconomyAgent _currentHighestBidder;

        [HideInInspector]
        public float currentAuctionTime;

        public float auctionTime = 5.0f;

        private bool _bidOn;

        private bool _bidLast;

        private bool _auctionOn;

        public float addChance = 0.05f;

        public float maxInventory = 50;

        private List<InventoryItem> _inventoryItems;

        public override float Progress => currentAuctionTime / auctionTime;

        private DataLogger Logger => GetComponent<DataLogger>();

        protected override AgentScreen ActionChoice => AgentScreen.Auction;

        public void Reset()
        {
            _inventoryItems?.Clear();
        }

        private void Start()
        {
            _inventoryItems = new List<InventoryItem>();
        }

        public void SetAuctionItem()
        {
            if (!_auctionOn)
            {
                _auctionOn = _inventoryItems.Count > 0;
                if (_auctionOn)
                {
                    System.Random rnd = new System.Random();

                    int index = rnd.Next(_inventoryItems.Count);

                    auctionedItem = _inventoryItems[index];

                    currentAuctionTime = 0.0f;

                    currentItemPrice = auctionedItem.baseBidPrice;
                }

                _currentHighestBidder = null;

                _bidOn = false;
                _bidLast = false;
            }
        }

        public void AddAuctionItem(InventoryItem item)
        {
            System.Random rand = new System.Random();
            double randValue = rand.NextDouble();
            float randChance = Mathf.Lerp(addChance, 0.02f, _inventoryItems.Count / maxInventory);
            if (randValue <= randChance)
            {
                _inventoryItems.Add(item);
            }
        }

        private void Update()
        {
            if (CurrentPlayers.Length > 0)
            {
                if (ItemCount > 0)
                {
                    SetAuctionItem();

                    currentAuctionTime += Time.deltaTime;
                    if (currentAuctionTime >= auctionTime)
                    {
                        if (!_bidOn)
                        {
                            AuctionOver();
                        }
                        else
                        {
                            _bidOn = false;
                        }
                        currentAuctionTime = 0.0f;
                    }
                }
                else
                {
                    ReturnToMain();
                }

                RequestDecisions();
            }
        }

        private void AuctionOver()
        {
            _auctionOn = false;
            _inventoryItems.Remove(auctionedItem);
            if (_bidLast)
            {
                Debug.Log("Sold Weapon: " + auctionedItem.itemName + " for " + currentItemPrice);
                _currentHighestBidder.BoughtItem(auctionedItem, currentItemPrice);
                Logger.AddAuctionItem(auctionedItem, currentItemPrice, _currentHighestBidder);
            }
            ReturnToMain();
        }

        protected override void RequestDecisions()
        {
            foreach (var agent in CurrentPlayers)
            {
                bool notHighest = !IsHighestBidder(agent);
                if (notHighest)
                {
                    agent.RequestDecision();
                }
            }
        }

        public void ReturnToMain()
        {
            foreach (var agent in CurrentPlayers)
            {
                playerInput.SetMainAction(agent, AgentScreen.Main);
            }
        }

        public override bool CanMove(EconomyAgent agent)
        {
            if (!_auctionOn)
            {
                return true;
            }
            else
            {
                return !(_currentHighestBidder == agent && _currentHighestBidder && agent);
            }
        }

        public bool IsHighestBidder(EconomyAgent agent)
        {
            return _currentHighestBidder == agent;
        }

        public void Bid(EconomyAgent agent)
        {
            var newPrice = currentItemPrice + bidIncrement;
            
            if (!IsHighestBidder(agent) && agent.Wallet.Money >= newPrice)
            {
                _currentHighestBidder = agent;
                currentItemPrice = newPrice;
                _bidOn = true;
                _bidLast = true;
            }
        }
    }
}
