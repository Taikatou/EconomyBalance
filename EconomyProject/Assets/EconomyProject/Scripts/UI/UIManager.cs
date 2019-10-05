using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class UiManager : MonoBehaviour
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
}
