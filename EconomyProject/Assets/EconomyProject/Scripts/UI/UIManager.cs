using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        public EconomyAgent playerAgent;
        public void StartAution()
        {
            playerAgent.SetAgentAction(AgentScreen.Auction);
        }

        public void StartQuest()
        {
            playerAgent.SetAgentAction(AgentScreen.Quest);
        }

        public void MainMenu()
        {
            playerAgent.SetAgentAction(AgentScreen.Main);
        }

        public void Bid()
        {
            playerAgent.SetChoice(AgentChoice.Bid);
        }
    }
}
