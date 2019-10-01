using System.Collections.Generic;
using UnityEngine;

public class GameAuction : EconomySystem
{
    private List<InventoryItem> _inventoryItems;

    public int ItemCount => _inventoryItems.Count;

    public float BidIncrement = 5.0f;

    public bool AuctionStarted = false;

    public InventoryItem AuctionedItem;

    public float CurrentItemPrice;

    public Dictionary<InventoryItem, int> SaleAttempt;

    public int SaleAttempts = 3;

    public void AddAuctionItem(InventoryItem item)
    {
        _inventoryItems.Add(item);
    }

    private void Start()
    {
        _inventoryItems = new List<InventoryItem>();

        SaleAttempt = new Dictionary<InventoryItem, int>();

        actionChoice = AgentActionChoice.Auction;
    }

    public bool AllReady
    {
        get
        {
            bool ready = true;
            foreach(var play in CurrentPlayers)
            {
                if(!play.Ready)
                {
                    ready = false;
                }
            }
            return ready;
        }
    }

    // Returns if the auction is over
    public bool RunAuction()
    {
        if(_inventoryItems.Count > 0)
        {
            if (!AuctionStarted)
            {
                AuctionStarted = true;
            }

            System.Random rnd = new System.Random();
            
            int index = rnd.Next(_inventoryItems.Count);

            AuctionedItem = _inventoryItems[index];

            bool sold = AuctionItem(AuctionedItem);

            bool toRemove = CheckResale(AuctionedItem, sold);

            if(toRemove)
            {
                _inventoryItems.Remove(AuctionedItem);
                SaleAttempt.Remove(AuctionedItem);
            }

            return _inventoryItems.Count == 0;
        }
        else
        {
            return true;
        }
    }

    private bool CheckResale(InventoryItem item, bool sold)
    {
        if (!sold)
        {
            bool contains = SaleAttempt.ContainsKey(AuctionedItem);
            if (contains)
            {
                SaleAttempt[item] = SaleAttempt[item] + 1;
            }
            else
            {
                SaleAttempt.Add(item, 1);
            }
            return SaleAttempt[item] > 3;
        }
        return true;
    }

    private bool AuctionItem(InventoryItem inventoryItem)
    {
        float itemPrice = inventoryItem.BaseBidPrice;
        EconomyAgent highestBidder = null;
        bool GotBid = false;

        bool bidComplete = false;
        while(bidComplete)
        {
            bidComplete = true;
            foreach (EconomyAgent agent in CurrentPlayers)
            {
                if (agent.Bid(inventoryItem, itemPrice))
                {
                    GotBid = true;
                    bidComplete = false;

                    highestBidder = agent;
                    itemPrice += BidIncrement;
                }
            }
        }

        if(GotBid)
        {
            AgentInventory aInventory = highestBidder.GetComponent<AgentInventory>();
            aInventory?.AddItem(inventoryItem);
        }
        return GotBid;
    }
}
