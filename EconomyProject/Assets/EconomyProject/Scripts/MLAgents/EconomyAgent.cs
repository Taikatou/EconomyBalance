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

    public enum AgentAct { Stay, Main, Quest, Auction }

    public enum AgentChoice { Ignore, Back, Bid }

    public class EconomyAgent : Agent
    {
        public AgentScreen chosenScreen = AgentScreen.Main;

        public double StartMoney;
        
        private double _money;

        public double Money => _money;

        public MainMenu mainMenu;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

        public InventoryItem Item => Inventory.EquipedItem;

        private GameAuction gameAuction => FindObjectOfType<GameAuction>();

        public static int AgentCounter = 0;

        public int AgentId;

        private void Start()
        {
            _money = StartMoney;
            AgentId = AgentCounter;
            AgentCounter++;
        }

        public void BoughtItem(InventoryItem item, float cost)
        {
            Inventory.AddItem(item);
            if(cost > 0)
            {
                _money -= cost;
            }
        }

        public int Damage
        {
            get
            {
                if(Item)
                {
                    return Item.damage;
                }
                return 0;
            }
        }

        public Dictionary<AgentAct, AgentScreen> ActionMap = new Dictionary<AgentAct, AgentScreen>()
        {
            { AgentAct.Main, AgentScreen.Main },
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

            if(chosenScreen == AgentScreen.Main)
            {
                SetAction(action);
            }
            else
            {
                SetChoice(action);
            }
        }

        private void SetAction(int action)
        {
            SetAgentAction((AgentAct) action);
        }

        public void SetAgentAction(AgentAct choice)
        {
            if(ActionMap.ContainsKey(choice))
            {
                chosenScreen = ActionMap[choice];
                if (choice != AgentAct.Stay)
                {
                    mainMenu.SwitchMenu(chosenScreen);
                }
            }
        }

        public void SetChoice(AgentChoice choice)
        {
            switch(choice)
            {
                case AgentChoice.Back:
                    SetAgentAction(AgentAct.Main);
                    break;
                case AgentChoice.Bid:
                    gameAuction.Bid(this);
                    break;
                case AgentChoice.Ignore:
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
            AddVectorObs(Damage);
            AddVectorObs(Item.unBreakable);
            AddVectorObs(Item.durability);
            AddVectorObs(gameAuction.ItemCount);
            AddVectorObs(Inventory.ItemCount);
            bool writed = false;
            if (chosenScreen == AgentScreen.Auction)
            {
                if(gameAuction.auctionedItem)
                {
                    writed = true;
                    AddVectorObs(gameAuction.auctionedItem.damage);
                    AddVectorObs(gameAuction.currentItemPrice);
                }
            }
            
            if(!writed)
            {
                AddVectorObs(0);
                AddVectorObs(0);
            }
        }

        public void EarnMoney(float amount)
        {
            if(amount > 0)
            {
                _money = Math.Round(_money + amount);
                AddReward((float)_money);
            }
        }
    }
}