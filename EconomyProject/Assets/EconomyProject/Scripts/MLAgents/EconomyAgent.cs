using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.UI;
using MLAgents;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public enum AgentScreen { Main, Quest, Auction }

    public enum AgentAct { Quest, Auction }

    public enum AgentChoice { Ignore, Bid }

    public class EconomyAgent : Agent
    {
        public AgentScreen chosenScreen = AgentScreen.Main;

        public double startMoney;
        
        private double _money;

        public double Money => _money;

        public MainMenu mainMenu;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

        public InventoryItem Item => Inventory.EquipedItem;

        private GameAuction gameAuction => FindObjectOfType<GameAuction>();

        private GameQuests gameQuests => FindObjectOfType<GameQuests>();

        public bool printObservations = false;

        public float Progress
        {
            get
            {
                switch (chosenScreen)
                {
                    case AgentScreen.Auction:
                        return gameAuction.Progress;
                    case AgentScreen.Quest:
                        return gameQuests.Progress;
                }
                return 0.0f;
            }
        }

        public static int agentCounter = 0;

        public int agentId;

        private void Start()
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

        public Dictionary<AgentAct, AgentScreen> actionMap = new Dictionary<AgentAct, AgentScreen>()
        {
            { AgentAct.Quest, AgentScreen.Quest },
            { AgentAct.Auction, AgentScreen.Auction }
        };

        public void DecreaseDurability()
        {
            Inventory.DecreaseDurability();
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            var action = Mathf.FloorToInt(vectorAction[0]);

            switch (chosenScreen)
            {
                case AgentScreen.Main:
                        SetAction(action);
                    break;
                case AgentScreen.Quest:
                    break;
                case AgentScreen.Auction:
                        SetChoice(action);
                    break;
            }
        }

        private void SetAction(int action)
        {
            SetAgentAction((AgentAct) action);
        }

        public void SetAgentAction(AgentAct choice)
        {
            if(actionMap.ContainsKey(choice))
            {
                bool canChange = CanMove();
                if (canChange)
                {
                    chosenScreen = actionMap[choice];
                    mainMenu.SwitchMenu(chosenScreen);
                }
            }
        }

        public bool CanMove()
        {
            switch(chosenScreen)
            {
                case AgentScreen.Auction:
                    return gameAuction.CanMove(this);
                case AgentScreen.Quest:
                    return gameQuests.CanMove(this);
            }
            return true;
        }

        public void SetChoice(AgentChoice choice)
        {
            switch(choice)
            {
                case AgentChoice.Ignore:
                    break;

                case AgentChoice.Bid:
                    gameAuction.Bid(this);
                    break;
            }
        }

        public void SetChoice(int choice)
        {
            SetChoice((AgentChoice)choice);
        }

        public override void CollectObservations()
        {
            AddVectorObs((int)chosenScreen);
            AddVectorObs((float)_money);
            AddVectorObs(Item.unBreakable);
            AddVectorObs(Item.durability);
            AddVectorObs(Item.efficiency);
            AddVectorObs(gameAuction.ItemCount);
            AddVectorObs(Inventory.ItemCount);
            AddVectorObs(Progress);

            if (printObservations)
            {
                Debug.Log(chosenScreen.ToString());
            }

            bool writed = false;
            if (chosenScreen == AgentScreen.Auction)
            {
                if(gameAuction.auctionedItem)
                {
                    writed = true;
                    AddVectorObs(gameAuction.currentItemPrice);
                    AddVectorObs(gameAuction.auctionedItem.efficiency);
                    AddVectorObs(gameAuction.IsHighestBidder(this));
                }
            }
            
            if(!writed)
            {
                AddVectorObs(0);
                AddVectorObs(0);
                AddVectorObs(0);
            }
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