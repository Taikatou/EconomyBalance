using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.UI;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public enum AgentActionChoice { Main, Quest, Auction }

    public enum AgentAuctionChoice { Bid, Ignore }

    public class EconomyAgent : Agent
    {
        public bool useUi = true;

        public AgentActionChoice chosenChoice;

        public AgentAuctionChoice auctionChoice;

        public float money;

        public MainMenu mainMenu;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

        public bool Bid(InventoryItem item, float price)
        {
            return price < money;
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            if(!useUi)
            {
                var action = Mathf.FloorToInt(vectorAction[0]);
                SetAction(action);
            }
        }

        private void SetAction(int action)
        {
            SetAgentAction((AgentActionChoice) action);
            SetRunningAction((AgentAuctionChoice) action);
        }

        public void SetAgentAction(AgentActionChoice choice)
        {
            chosenChoice = choice;
            mainMenu.SwitchMenu(choice);
        }

        public void SetRunningAction(AgentAuctionChoice choice)
        {
            auctionChoice = choice;
        }

        public override void CollectObservations()
        {
            
        }
    }
}