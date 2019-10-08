﻿using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class GameAuction : EconomySystem
    {
        private List<InventoryItem> _inventoryItems;

        public int ItemCount => _inventoryItems.Count;

        public float bidIncrement = 5.0f;

        [HideInInspector]
        public InventoryItem auctionedItem;

        [HideInInspector]
        public float currentItemPrice;

        [HideInInspector]
        public EconomyAgent currentHighestBidder;

        private float _auctionTime = 5.0f;

        [HideInInspector]
        public float currentAuctionTime;

        public float Progress => currentAuctionTime / _auctionTime;

        public bool bidOn;

        public bool bidLast;

        public bool auctionOn;

        public void SetAuctionItem()
        {
            auctionOn = _inventoryItems.Count > 0;
            if (auctionOn)
            {
                System.Random rnd = new System.Random();

                int index = rnd.Next(_inventoryItems.Count);

                auctionedItem = _inventoryItems[index];

                currentAuctionTime = 0.0f;

                currentItemPrice = auctionedItem.baseBidPrice;

                bidOn = false;
                bidLast = false;
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
                if (!auctionOn)
                {
                    SetAuctionItem();
                }
                currentAuctionTime += Time.deltaTime;
                if (currentAuctionTime >= _auctionTime)
                {
                    currentAuctionTime = 0.0f;
                    if (!bidOn)
                    {
                        _inventoryItems.Remove(auctionedItem);
                        SetAuctionItem();
                        if(bidLast)
                        {
                            currentHighestBidder.BoughtItem(auctionedItem, currentItemPrice);
                        }
                    }
                    else
                    {
                        bidOn = false;
                    }
                }
            }
        }

        public void Bid(EconomyAgent player)
        {
            if(player != currentHighestBidder)
            {
                float newPrice = currentItemPrice + bidIncrement;
                if (player.money >= newPrice)
                {
                    currentHighestBidder = player;
                    currentItemPrice = newPrice;
                    bidOn = true;
                    bidLast = true;
                }
            }
        }
    }
}
