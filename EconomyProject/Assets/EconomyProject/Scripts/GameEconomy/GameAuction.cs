using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class GameAuction : EconomySystem
    {
        private List<InventoryItem> _inventoryItems;

        public int ItemCount => _inventoryItems.Count;

        public float bidIncrement = 5.0f;

        public InventoryItem auctionedItem;

        public float currentItemPrice;

        public EconomyAgent currentHighestBidder;

        private float _auctionTime = 3.0f;

        public float currentAuctionTime;

        public bool sold;

        public void SetupAuction()
        {
            currentAuctionTime = 0.0f;
            SetAuctionItem();
            sold = false;
        }

        public void SetAuctionItem()
        {
            if (_inventoryItems.Count > 0)
            {
                System.Random rnd = new System.Random();

                int index = rnd.Next(_inventoryItems.Count);

                auctionedItem = _inventoryItems[index];
            }
        }

        public void AddAuctionItem(InventoryItem item)
        {
            _inventoryItems.Add(item);
        }

        private void Start()
        {
            _inventoryItems = new List<InventoryItem>();

            actionChoice = AgentActionChoice.Auction;
        }

        public bool AllReady
        {
            get
            {
                bool ready = true;
                foreach (var play in CurrentPlayers)
                {
                    if (!play.ready)
                    {
                        ready = false;
                    }
                }

                return ready;
            }
        }

        // Returns if the auction is over
        public bool RunAuction(float time)
        {
            if (_inventoryItems.Count > 0)
            {
                currentAuctionTime += time;
                if (currentAuctionTime >= _auctionTime)
                {
                    _inventoryItems.Remove(auctionedItem);

                    SetAuctionItem();
                }
                return _inventoryItems.Count == 0;
            }
            return true;
        }

        public void Bid(EconomyAgent player)
        {
            currentHighestBidder = player;
            currentItemPrice += bidIncrement;
            sold = true;
        }
    }
}
