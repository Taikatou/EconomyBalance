using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.UI;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public enum AgentScreen { Main, Quest, Auction }

    public enum AgentChoice {  Back, Bid, Ignore }

    public class EconomyAgent : Agent
    {
        public bool useUi = true;

        public AgentScreen chosenChoice;

        public float money;

        public MainMenu mainMenu;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

        public int Damage => Inventory.Damage;

        private GameAuction gameAuction => FindObjectOfType<GameAuction>();

        public bool Bid(InventoryItem item, float price)
        {
            return price < money;
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            if(!useUi)
            {
                var action = Mathf.FloorToInt(vectorAction[0]);

                if(chosenChoice == AgentScreen.Main)
                {
                    SetAction(action);
                }
                else
                {
                    SetChoice(action);
                }
            }
        }

        private void SetAction(int action)
        {
            SetAgentAction((AgentScreen) action);
        }

        public void SetAgentAction(AgentScreen choice)
        {
            chosenChoice = choice;
            mainMenu.SwitchMenu(choice);
        }

        public void SetChoice(AgentChoice choice)
        {
            switch(choice)
            {
                case AgentChoice.Back:
                    SetAgentAction(AgentScreen.Main);
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
    }
}