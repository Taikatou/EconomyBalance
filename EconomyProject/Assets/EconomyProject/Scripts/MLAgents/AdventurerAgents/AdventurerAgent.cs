using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.Inventory;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public enum AgentScreen { Main, Quest, Auction, Shop }

    public enum AuctionChoice { Ignore, Bid }

    [RequireComponent(typeof(AgentInventory))]
    [RequireComponent(typeof(AdventurerInventory))]
    [RequireComponent(typeof(EconomyWallet))]
    public class AdventurerAgent : Observations
    {
        public bool printObservations = false;

        public bool canSeeDistribution = true;

        public AgentInventory inventory;

        public AdventurerInventory adventurerInventory;

        public EconomyWallet wallet;

        public GameAuction gameAuction;

        public PlayerInput playerInput;

        public InventoryItem Item => adventurerInventory.EquipedItem;

        public AgentScreen ChosenScreen => playerInput.GetScreen(this);

        public override void AgentReset()
        {
            var reset = GetComponentInParent<ResetScript>();
            reset.Reset();
        }

        public void ResetEconomyAgent()
        {
            inventory.ResetInventory();
            wallet.Reset();
        }

        public void BoughtItem(InventoryItem item, float cost)
        {
            inventory.AddItem(item);
            wallet.SpendMoney(cost);
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            var mainAction = Mathf.FloorToInt(vectorAction[0]);
            var auctionAction = Mathf.FloorToInt(vectorAction[1]);

            if (printObservations)
            {
                Debug.Log("MainAction: " + mainAction + " AuctionAction: " + auctionAction);
            }
            AgentAction(mainAction, auctionAction);
        }

        public virtual void AgentAction(int mainAction, int auctionAction)
        {
            playerInput.SetAgentAction(this, mainAction, auctionAction);
        }

        private string AddAuctionObs(InventoryItem item)
        {
            var output = AddVectorObs(item);
            var highestBidder = gameAuction.IsHighestBidder(this);
            output += AddVectorObs(highestBidder, "Is Highest Bidder");
            output += AddVectorObs(gameAuction.currentItemPrice, "Current Price");
            AddVectorObs(gameAuction.BidLast);
            AddVectorObs(gameAuction.BidOn);

            return output;
        }

        protected string AddVectorObs(AgentScreen observation, string obsName)
        {
            AddVectorObs((int)observation);
            return obsName + ": " + observation;
        }

        public override void CollectObservations()
        {
            var output = AddVectorObs(ChosenScreen, "Chosen Screen");
            output += AddVectorObs(wallet ? (float)wallet.Money : 0.0f, "Money");
            output += AddVectorObs(Item);
            output += AddVectorObs(gameAuction.ItemCount, "Auction Item Count");
            output += AddVectorObs(inventory.ItemCount, "Inventory Item Count");
            output += AddVectorObs(playerInput.GetProgress(this), "Progress");
            output += AddVectorObs(gameAuction.currentItemPrice, "Current Item Price");
            output += AddVectorObs(canSeeDistribution ? playerInput.NumberInAuction : 0, "Number in auction");
            output += AddVectorObs(canSeeDistribution ? playerInput.NumberInQuest : 0, "Number in quest");

            output += AddAuctionObs(gameAuction.auctionedItem);

            if (printObservations)
            {
                Debug.Log(output);
            }
        }
    }
}