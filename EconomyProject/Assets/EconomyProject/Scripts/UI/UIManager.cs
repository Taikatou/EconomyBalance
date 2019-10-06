using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        public EconomyAgent playerAgent;
        public void StartAution()
        {
            playerAgent.SetAgentAction(AgentAct.Auction);
        }

        public void StartQuest()
        {
            playerAgent.SetAgentAction(AgentAct.Quest);
        }

        public void MainMenu()
        {
            playerAgent.SetAgentAction(AgentAct.Main);
        }

        public void Bid()
        {
            playerAgent.SetChoice(AgentChoice.Bid);
        }
    }
}
