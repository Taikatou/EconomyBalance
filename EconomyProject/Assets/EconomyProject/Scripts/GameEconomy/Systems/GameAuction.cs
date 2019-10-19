using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents;
using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems
{
    public class GameAuction : EconomySystem
    {
        private HashSet<EconomyAgent> _agentBids;
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

        private bool _auctionOn = false;

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
            _agentBids = new HashSet<EconomyAgent>();
            _inventoryItems = new List<InventoryItem>();
        }

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

                _agentBids.Clear();
            }

            _currentHighestBidder = null;

            _bidOn = false;
            _bidLast = false;
        }

        public void AddAuctionItem(InventoryItem item)
        {
            _inventoryItems.Add(item);
        }

        private void Update()
        {
            if (ItemCount > 0 && CurrentPlayers.Length > 0)
            {
                if (!_auctionOn)
                {
                    SetAuctionItem();
                }
                currentAuctionTime += Time.deltaTime;
                if (currentAuctionTime >= auctionTime)
                {
                    currentAuctionTime = 0.0f;
                    if (!_bidOn)
                    {
                        _inventoryItems.Remove(auctionedItem);
                        if(_bidLast)
                        {
                            _currentHighestBidder.BoughtItem(auctionedItem, currentItemPrice);
                            Logger.AddAuctionItem(auctionedItem, currentItemPrice, _currentHighestBidder);

                        }
                        SetAuctionItem();
                    }
                    else
                    {
                        _bidOn = false;
                    }
                }

                foreach (var agent in CurrentPlayers)
                {
                    bool alreadyBid = _agentBids.Contains(agent);
                    if (!alreadyBid)
                    {
                        agent.RequestDecision();
                    }
                }
            }
            else
            {
                foreach (var agent in CurrentPlayers)
                {
                    PlayerInput.SetMainAction(agent, AgentScreen.Main);
                }
            }
        }

        public override bool CanMove(EconomyAgent agent)
        {
            return !(_currentHighestBidder == agent && _currentHighestBidder && agent);
        }

        public bool IsHighestBidder(EconomyAgent agent)
        {
            return _currentHighestBidder == agent;
        }

        public void Bid(EconomyAgent player)
        {
            var newPrice = currentItemPrice + bidIncrement;
            
            if (!IsHighestBidder(player) && player.Wallet.Money >= newPrice)
            {
                _currentHighestBidder = player;
                currentItemPrice = newPrice;
                _bidOn = true;
                _bidLast = true;
            }
        }

        public void MakeDecision(EconomyAgent agent)
        {
            if (!_agentBids.Contains(agent))
            {
                _agentBids.Add(agent);
            }
        }
    }
}
