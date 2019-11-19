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
            var playerInput = accessor.PlayerInput;
            if (accessor.CurrentAgent != null && playerInput != null)
            {
                var screen = playerInput.GetScreen(accessor.CurrentAgent);
                if (screen != _cacheAgentScreen)
                {
                    _cacheAgentScreen = screen;
                    SwitchMenu(screen);
                }
            }
        }

        public void StartAuction()
        {
            PlayerInput.SetMainAction(accessor.CurrentAgent, AgentScreen.Auction);
        }

        public void StartQuest()
        {
            PlayerInput.SetMainAction(accessor.CurrentAgent, AgentScreen.Quest);
        }

        public void MainMenu()
        {
            PlayerInput.SetMainAction(accessor.CurrentAgent, AgentScreen.Main);
        }

        public void Bid()
        {
            PlayerInput.SetAuctionChoice(accessor.CurrentAgent, AuctionChoice.Bid);
        }
    }
}
