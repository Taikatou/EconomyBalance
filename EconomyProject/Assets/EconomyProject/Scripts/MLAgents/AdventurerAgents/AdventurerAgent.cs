﻿using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.Inventory;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public enum AgentScreen { Main, Quest, Auction, Shop }

    public enum AuctionChoice { Ignore, Bid }

    public class AdventurerAgent : Agent
    {
        public InventoryItem endItem;

        public bool resetOnComplete = false;

        public bool punishLoss = false;

        public bool printObservations = false;

        public bool canSeeDistribution = true;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

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
            var output = AddVectorObs(item);
            output += AddVectorObs(true, GameAuction.IsHighestBidder(this), "Is Highest Bidder");
            output += AddVectorObs(GameAuction.currentItemPrice, "Current Price");
            AddVectorObs(GameAuction.BidLast);
            AddVectorObs(GameAuction.BidOn);

            return output;
        }

        protected string AddVectorObs(float observation, string obsName)
        {
            AddVectorObs(observation);
            return " " +  obsName + ": " + observation;
        }

        protected string AddVectorObs(bool valid, bool observation, string obsName)
        {
            if (valid)
            {
                AddVectorObs(observation? 1: 2);
            }
            else
            {
                AddVectorObs(0);
            }
            
            return " " + obsName + ": " + observation;
        }

        private string AddVectorObs(InventoryItem item, bool condition = true, float defaultObs = 0.0f)
        {
            condition = condition && item;
            var output = " Current Item";
            output += AddVectorObs(condition ? WeaponId.GetWeaponId(item.itemName) : -1, "ItemName");
            output += AddVectorObs(condition ? item.durability : defaultObs, "Durability");
            output += AddVectorObs(condition ? item.baseDurability : defaultObs, "Base Durability");
            output += AddVectorObs(condition ? item.numLootSpawns : defaultObs, "Num Loot Spawn");
            output += AddVectorObs(condition ? item.efficiency : defaultObs, "Efficiency");
            output += AddVectorObs(condition, item && item.unBreakable, "Unbreakable");

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
            output += AddVectorObs(Wallet? (float)Wallet.Money : 0.0f, "Money");
            output += AddVectorObs(Item);
            output += AddVectorObs(GameAuction.ItemCount, "Auction Item Count");
            output += AddVectorObs(Inventory.ItemCount, "Inventory Item Count");
            output += AddVectorObs(PlayerInput.GetProgress(this), "Progress");
            output += AddVectorObs(GameAuction.currentItemPrice, "Current Item Price");
            output += AddVectorObs(canSeeDistribution ? PlayerInput.NumberInAuction : 0, "Number in auction");
            output += AddVectorObs(canSeeDistribution ? PlayerInput.NumberInQuest : 0, "Number in quest");

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
    }
}