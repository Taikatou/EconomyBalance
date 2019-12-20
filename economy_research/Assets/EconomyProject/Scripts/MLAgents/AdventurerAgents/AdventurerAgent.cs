using EconomyProject.Scripts.GameEconomy;
using EconomyProject.Scripts.GameEconomy.Systems;
using EconomyProject.Scripts.Inventory;
using MLAgents;
using UnityEngine;

namespace EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public enum AgentScreen { Main, Quest, Auction, Shop }

    public enum AuctionChoice { Ignore, Bid }

    [RequireComponent(typeof(AgentInventory))]
    [RequireComponent(typeof(AdventurerInventory))]
    [RequireComponent(typeof(EconomyWallet))]
    public class AdventurerAgent : Agent
    {
        public AgentInventory inventory;

        public AdventurerInventory adventurerInventory;

        public EconomyWallet wallet;

        public GameAuction gameAuction;

        public PlayerInput playerInput;

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

        public override void AgentAction(float[] vectorAction)
        {
            var mainAction = Mathf.FloorToInt(vectorAction[0]);
            var auctionAction = Mathf.FloorToInt(vectorAction[1]);
            
            AgentAction(mainAction, auctionAction);
        }

        protected virtual void AgentAction(int mainAction, int auctionAction)
        {
            playerInput.SetAgentAction(this, mainAction, auctionAction);
        }

        public override void CollectObservations()
        {
            // Player Observations
            AddVectorObs((int)ChosenScreen);
            AddVectorObs(wallet ? (float)wallet.Money : 0.0f);
            AddVectorObs(adventurerInventory.EquipedItem);
            
            // Player Input Observations
            AddVectorObs(playerInput.GetProgress(this));
            AddVectorObs(playerInput.GetNumberInAuction());
            AddVectorObs(playerInput.GetNumberInQuest());
            
            // Auction Observations
            var highestBidder = gameAuction.IsHighestBidder(this);
            
            AddVectorObs(gameAuction.auctionedItem);
            AddVectorObs(highestBidder);
            AddVectorObs(gameAuction.currentItemPrice);
            AddVectorObs(gameAuction.BidLast);
            AddVectorObs(gameAuction.BidOn);
        }
        
        private void AddVectorObs(InventoryItem item, bool condition = true, float defaultObs = 0.0f)
        {
            condition = condition && item;
            AddVectorObs(condition ? WeaponId.GetWeaponId(item.itemName) : -1);
            AddVectorObs(condition ? item.durability : defaultObs);
            AddVectorObs(condition ? item.numLootSpawns : defaultObs);
            AddVectorObs(condition ? item.efficiency : defaultObs);
            AddVectorObs(condition ? (item.unBreakable ? 1 : 2): 0);
        }
    }
}