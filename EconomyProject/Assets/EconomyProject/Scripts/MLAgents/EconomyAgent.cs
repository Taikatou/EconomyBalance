using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.UI;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public enum AgentActionChoice { Quest, Auction }

    public enum AgentAuctionChoice { Bid, Ignore }

    public class EconomyAgent : Agent
    {
        public bool useUi = true;

        public bool ready = false;

        public AgentActionChoice chosenChoice;

        public AgentAuctionChoice auctionChoice;

        public float money;

        public MainMenu mainMenu;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

        public GameEconomy.GameEconomy GEconomy => FindObjectOfType<GameEconomy.GameEconomy>();

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
            if(GEconomy.state == GameState.ChooseAction)
            {
                chosenChoice = choice;
                MakeReady();
            }
        }

        public void SetRunningAction(AgentAuctionChoice choice)
        {
            if (GEconomy.state == GameState.AuctionOrQuest)
            {
                auctionChoice = choice;
                switch (auctionChoice)
                {
                    case AgentAuctionChoice.Bid:
                        GEconomy.GameAuction.Bid(this);
                        break;
                }
            }
        }

        private void MakeReady()
        {
            ready = true;
        }

        public override void CollectObservations()
        {
            GameState gState = GEconomy.state;
            AddVectorObs(System.Convert.ToInt32(gState));
            AddVectorObs(GEconomy.GameAuction.ItemCount);
            bool collected = false;
            if (gState != GameState.AuctionOrQuest)
            {
                InventoryItem item = GEconomy.GameAuction.auctionedItem;
                if(item)
                {
                    AddVectorObs(item.dmg);
                    AddVectorObs(GEconomy.GameAuction.currentItemPrice);
                    collected = true;
                }
            }
            if(!collected)
            {
                AddVectorObs(0.0f);
                AddVectorObs(0.0f);
            }
        }

        public void SetUi()
        {
            switch (chosenChoice)
            {
                case AgentActionChoice.Auction:
                    mainMenu?.SwitchToAuction();
                    break;
                case AgentActionChoice.Quest:
                    mainMenu?.SwitchToMain();
                    break;
            }
        }
    }
}