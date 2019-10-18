using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class UiManager : MonoBehaviour
    {
        public EconomyAgent playerAgent;
        public EconomyAcademy economyAcademy;

        public PlayerInput PlayerInput => FindObjectOfType<PlayerInput>();

        public void StartAution()
        {
            PlayerInput.SetMainAction(playerAgent, AgentScreen.Auction);
        }

        public void StartQuest()
        {
            PlayerInput.SetMainAction(playerAgent, AgentScreen.Quest);
        }

        public void MainMenu()
        {
            PlayerInput.SetMainAction(playerAgent, AgentScreen.Main);
        }

        public void Bid()
        {
            PlayerInput.SetAuctionChoice(playerAgent, AuctionChoice.Bid);
        }

        public void Reset()
        {
            economyAcademy.AcademyReset();
        }
    }
}
