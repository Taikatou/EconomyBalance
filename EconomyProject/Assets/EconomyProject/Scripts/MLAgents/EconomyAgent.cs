using MLAgents;
using UnityEngine;

public enum AgentActionChoice { Quest, Auction }

public enum AgentAuctionChoice { Bid, Ignore }

public class EconomyAgent : Agent
{
    public bool UseUI = true;

    public bool Ready = false;

    public AgentActionChoice ChoosenChoice;

    public AgentAuctionChoice AuctionChoice;

    public float Money;

    public GameEconomy GEconomy => FindObjectOfType<GameEconomy>();

    public bool Bid(InventoryItem item, float price)
    {
        if (price < Money)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if(!UseUI)
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
        if(GEconomy.State == GameState.ChooseAction)
        {
            ChoosenChoice = choice;
            MakeReady();
        }
    }

    public void SetRunningAction(AgentAuctionChoice choice)
    {
        if (GEconomy.State == GameState.AuctionOrQuest)
        {
            AuctionChoice = choice;
            MakeReady();
        }
    }

    private void MakeReady()
    {
        Ready = true;
    }

    public override void CollectObservations()
    {
        GameState gState = GEconomy.State;
        AddVectorObs(System.Convert.ToInt32(gState));
        AddVectorObs(GEconomy.GameAuction.ItemCount);
        bool collected = false;
        if (gState != GameState.AuctionOrQuest)
        {
            InventoryItem item = GEconomy.GameAuction.AuctionedItem;
            if(item)
            {
                AddVectorObs(item.Dmg);
                AddVectorObs(GEconomy.GameAuction.CurrentItemPrice);
                collected = true;
            }
        }
        if(!collected)
        {
            AddVectorObs(0.0f);
            AddVectorObs(0.0f);
        }
    }
}
