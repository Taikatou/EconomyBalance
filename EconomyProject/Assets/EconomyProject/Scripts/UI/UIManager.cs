using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public EconomyAgent playerAgent;
    public void StartAution()
    {
        playerAgent.SetAgentAction(AgentActionChoice.Auction);
    }

    public void StartQuest()
    {
        playerAgent.SetAgentAction(AgentActionChoice.Quest);
    }

    public void Bid()
    {
        playerAgent.SetRunningAction(AgentAuctionChoice.Bid);
    }

    public void Ignore()
    {
        playerAgent.SetRunningAction(AgentAuctionChoice.Ignore);
    }
}
