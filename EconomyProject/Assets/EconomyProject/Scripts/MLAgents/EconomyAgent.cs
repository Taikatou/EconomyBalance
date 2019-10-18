using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.Inventory;
using MLAgents;
using System;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public enum AgentScreen { Main, Quest, Auction }

    public enum AuctionChoice { Ignore, Bid }

    public class EconomyAgent : Agent
    {
        public double startMoney;
        
        private double _money;

        public double Money => _money;

        public static int agentCounter = 0;

        public int agentId;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

        public InventoryItem Item => Inventory.EquipedItem;

        private GameAuction gameAuction => FindObjectOfType<GameAuction>();

        private GameQuests gameQuests => FindObjectOfType<GameQuests>();

        public PlayerInput PlayerInput => FindObjectOfType<PlayerInput>();

        public AgentScreen ChosenScreen => PlayerInput.GetScreen(this);

        public override void AgentReset()
        {
            _money = 0;
            Inventory.ResetInventory();
        }

        public override void InitializeAgent()
        {
            _money = startMoney;
            agentId = agentCounter;
            agentCounter++;
        }

        public void BoughtItem(InventoryItem item, float cost)
        {
            Inventory.AddItem(item);
            if(cost > 0)
            {
                _money -= cost;
            }
        }

        public void DecreaseDurability()
        {
            Inventory.DecreaseDurability();
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            var action = Mathf.FloorToInt(vectorAction[0]);
            PlayerInput.SetAgentAction(this, action);
        }

        public virtual void OnSwitch() { }

        private void AddAuctionObs(InventoryItem item)
        {
            bool inAuction = (ChosenScreen == AgentScreen.Auction) && item;

            AddVectorObs(inAuction && gameAuction.IsHighestBidder(this));
            
            AddVectorObs(inAuction ? gameAuction.currentItemPrice : 0.0f);
            AddVectorObs(item, inAuction);
        }

        private void AddVectorObs(InventoryItem item, bool condition=true, float defaultObs=0.0f, bool defaultBool=false)
        {
            AddVectorObs(condition ? item.durability : defaultObs);
            AddVectorObs(condition ? item.efficiency : defaultObs);
            AddVectorObs(condition ? item.unBreakable : defaultBool);
        }

        public override void CollectObservations()
        {
            AddVectorObs((int)ChosenScreen);
            AddVectorObs((float)_money);
            AddVectorObs(Item);
            AddVectorObs(gameAuction.ItemCount);
            AddVectorObs(Inventory.ItemCount);
            AddVectorObs(PlayerInput.GetProgress(this));

            AddAuctionObs(gameAuction.auctionedItem);
        }

        public void EarnMoney(float amount)
        {
            if(amount > 0)
            {
                _money = Math.Round(_money + amount);
                var reward = _money / gameQuests.MaxMoney;
                AddReward((float)reward);
            }
        }
    }
}