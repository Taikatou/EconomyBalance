using System.Collections.Generic;
using System.Linq;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
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

        [HideInInspector]
        public float currentAuctionTime;

        public float auctionTime = 5.0f;

        private AdventurerAgent _currentHighestBidder;

        private bool _auctionOn;

        public float addChance = 0.3f;

        public float maxInventory = 50;

        private List<InventoryItem> _inventoryItems;

        public override float Progress => currentAuctionTime / auctionTime;

        private DataLogger Logger => GetComponent<DataLogger>();

        protected override AgentScreen ActionChoice => AgentScreen.Auction;

        public bool BidOn { get; private set; }

        public bool BidLast { get; private set; }

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

                BidOn = false;
                BidLast = false;
            }
        }

        public virtual float AddChance => Mathf.Lerp(addChance, 0.005f, _inventoryItems.Count / maxInventory);

        public void AddAuctionItem(InventoryItem item)
        {
            var rand = new System.Random();
            var randValue = rand.NextDouble();
            var randChance = 0.99;
            if (randValue <= randChance)
            {
                _inventoryItems.Add(item);
            }
        }

        private void FixedUpdate()
        {
            if (CurrentPlayers.Length > 0)
            {
                if (ItemCount > 0)
                {
                    SetAuctionItem();
                    currentAuctionTime += Time.deltaTime;
                    if (currentAuctionTime >= auctionTime)
                    {
                        if (!BidOn)
                        {
                            AuctionOver();
                        }
                        else
                        {
                            BidOn = false;
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
            if (BidLast)
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

        public override bool CanMove(AdventurerAgent agent)
        {
            if (!_auctionOn)
            {
                return true;
            }
            return !(_currentHighestBidder == agent && _currentHighestBidder && agent);
        }

        public bool IsHighestBidder(AdventurerAgent agent)
        {
            return _currentHighestBidder == agent;
        }

        public void Bid(AdventurerAgent agent)
        {
            if (CurrentPlayers.Contains(agent))
            {
                var newPrice = currentItemPrice + bidIncrement;

                if (!IsHighestBidder(agent) && agent.wallet.Money >= newPrice)
                {
                    _currentHighestBidder = agent;
                    currentItemPrice = newPrice;
                    BidOn = true;
                    BidLast = true;
                }
            }
        }
    }
}
