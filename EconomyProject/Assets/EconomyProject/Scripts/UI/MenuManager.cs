using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject auctionMenu;

        public GameObject questMenu;

        public GameObject mainMenu;

        private AgentScreen _cacheAgentScreen = AgentScreen.Main;

        public UiAccessor accessor;

        public PlayerInput PlayerInput => accessor.PlayerInput;

        public void SwitchMenu(AgentScreen whichMenu)
        {
            auctionMenu?.SetActive(whichMenu == AgentScreen.Auction);
            mainMenu?.SetActive(whichMenu == AgentScreen.Main);
            questMenu?.SetActive(whichMenu == AgentScreen.Quest);
        }

        private void Update()
        {
            PlayerInput playerInput = accessor.PlayerInput;
            if (accessor.AdventurerAgent != null && playerInput != null)
            {
                AgentScreen screen = playerInput.GetScreen(accessor.AdventurerAgent);
                if (screen != _cacheAgentScreen)
                {
                    _cacheAgentScreen = screen;
                    SwitchMenu(screen);
                }
            }
        }

        public void StartAuction()
        {
            PlayerInput.SetMainAction(accessor.AdventurerAgent, AgentScreen.Auction);
        }

        public void StartQuest()
        {
            PlayerInput.SetMainAction(accessor.AdventurerAgent, AgentScreen.Quest);
        }

        public void MainMenu()
        {
            PlayerInput.SetMainAction(accessor.AdventurerAgent, AgentScreen.Main);
        }

        public void Bid()
        {
            PlayerInput.SetAuctionChoice(accessor.AdventurerAgent, AuctionChoice.Bid);
        }
    }
}
