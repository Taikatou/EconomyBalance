using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.MLAgents;
using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject auctionMenu;

        public GameObject questMenu;

        public GameObject mainMenu;

        public EconomyAgent economyAgent;

        public static EconomyAgent economyAgentInstance;

        private AgentScreen _cacheAgentScreen = AgentScreen.Main;

        private EconomyAcademy EconomyAcademy => FindObjectOfType<EconomyAcademy>();

        public PlayerInput PlayerInput => FindObjectOfType<PlayerInput>();

        public void SwitchMenu(AgentScreen whichMenu)
        {
            auctionMenu?.SetActive(whichMenu == AgentScreen.Auction);
            mainMenu?.SetActive(whichMenu == AgentScreen.Main);
            questMenu?.SetActive(whichMenu == AgentScreen.Quest);
        }

        private void Update()
        {
            PlayerInput playerInput = FindObjectOfType<PlayerInput>();
            if (economyAgent != null && playerInput != null)
            {
                AgentScreen screen = playerInput.GetScreen(economyAgent);
                if (screen != _cacheAgentScreen)
                {
                    _cacheAgentScreen = screen;
                    SwitchMenu(screen);
                }
            }
        }

        private void Start()
        {
            if (economyAgent == null)
            {
                economyAgentInstance = FindObjectOfType<EconomyAgent>();
            }
            else
            {
                economyAgentInstance = economyAgent;
            }
        }

        public void StartAuction()
        {
            PlayerInput.SetMainAction(economyAgent, AgentScreen.Auction);
        }

        public void StartQuest()
        {
            PlayerInput.SetMainAction(economyAgent, AgentScreen.Quest);
        }

        public void MainMenu()
        {
            PlayerInput.SetMainAction(economyAgent, AgentScreen.Main);
        }

        public void Bid()
        {
            PlayerInput.SetAuctionChoice(economyAgent, AuctionChoice.Bid);
        }

        public void Reset()
        {
            EconomyAcademy.AcademyReset();
        }
    }
}
