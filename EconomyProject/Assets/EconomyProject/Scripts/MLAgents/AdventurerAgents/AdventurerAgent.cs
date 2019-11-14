using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.Inventory;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public enum AgentScreen { Main, Quest, Auction, Shop }

    public enum AuctionChoice { Ignore, Bid }

    public class AdventurerAgent : Agent, IObsAgent
    {
        public InventoryItem endItem;

        public bool resetOnComplete = false;

        public bool punishLoss = false;

        public bool printObservations = false;

        public bool canSeeDistribution = true;

        public AdventurerInventory Inventory => GetComponent<AdventurerInventory>();

        public InventoryItem Item => Inventory.EquipedItem;

        public GameAuction GameAuction => GetComponentInParent<AdventurerSpawner>().gameAuction;

        public PlayerInput PlayerInput => GetComponentInParent<AdventurerSpawner>().playerInput;

        public AgentScreen ChosenScreen => PlayerInput.GetScreen(this);

        public EconomyWallet Wallet => GetComponent<EconomyWallet>();

        public override void AgentReset()
        {
            var reset = GetComponentInParent<ResetScript>();
            reset.Reset();
        }

        public void ResetEconomyAgent()
        {
            Inventory.ResetInventory();
            Wallet.Reset();
        }


        public void BoughtItem(InventoryItem item, float cost)
        {
            Inventory.AddItem(item);
            Wallet?.SpendMoney(cost);

            if (resetOnComplete && item && endItem)
            {
                if (item.itemName == endItem.itemName)
                {
                    Done();
                }
            }
            AddReward(item.efficiency / endItem.efficiency);
        }

        public void DecreaseDurability()
        {
            Inventory.DecreaseDurability();
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
            PlayerInput.SetAgentAction(this, mainAction, auctionAction);
        }

        private string AddAuctionObs(InventoryItem item)
        {
            var output = Observations.AddVectorObs(this, item);
            output += Observations.AddVectorObs(this, true, GameAuction.IsHighestBidder(this), "Is Highest Bidder");
            output += Observations.AddVectorObs(this, GameAuction.currentItemPrice, "Current Price");
            AddVectorObs(GameAuction.BidLast);
            AddVectorObs(GameAuction.BidOn);

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
            output += Observations.AddVectorObs(this, Wallet ? (float)Wallet.Money : 0.0f, "Money");
            output += Observations.AddVectorObs(this, Item);
            output += Observations.AddVectorObs(this, GameAuction.ItemCount, "Auction Item Count");
            output += Observations.AddVectorObs(this, Inventory.ItemCount, "Inventory Item Count");
            output += Observations.AddVectorObs(this, PlayerInput.GetProgress(this), "Progress");
            output += Observations.AddVectorObs(this, GameAuction.currentItemPrice, "Current Item Price");
            output += Observations.AddVectorObs(this, canSeeDistribution ? PlayerInput.NumberInAuction : 0, "Number in auction");
            output += Observations.AddVectorObs(this, canSeeDistribution ? PlayerInput.NumberInQuest : 0, "Number in quest");

            output += AddAuctionObs(GameAuction.auctionedItem);

            if (printObservations)
            {
                Debug.Log(output);
            }
        }

        public void EarnMoney(float amount)
        {
            Wallet.EarnMoney(amount);
            var reward = (amount / endItem.rewardPrice) / 5;
        }

        public void LoseMoney(float amount, float punishment=0.1f)
        {
            if (punishLoss)
            {
                Wallet.LoseMoney(amount);
            }
        }

        public new void AddVectorObs(float observation)
        {
            base.AddVectorObs(observation);
        }

        public new void AddVectorObs(int observation)
        {
            base.AddVectorObs(observation);
        }

        public new void AddVectorObs(bool observation)
        {
            base.AddVectorObs(observation);
        }
    }
}