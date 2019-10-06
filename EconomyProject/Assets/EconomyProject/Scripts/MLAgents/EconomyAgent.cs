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

        public double money;

        public MainMenu mainMenu;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

        public int Damage => Inventory.Damage;

        private GameAuction gameAuction => FindObjectOfType<GameAuction>();

        public Dictionary<AgentAct, AgentScreen> ActionMap = new Dictionary<AgentAct, AgentScreen>()
        {
            { AgentAct.Main, AgentScreen.Main },
            { AgentAct.Quest, AgentScreen.Quest },
            { AgentAct.Auction, AgentScreen.Auction }
        };

        public bool Bid(InventoryItem item, float price)
        {
            return price < money;
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
            
        }

        public void EarnMoney(float amount)
        {
            if(amount > 0)
            {
                money = Math.Round(money + amount);
            }
        }
    }
}