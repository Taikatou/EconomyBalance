using System.Collections.Generic;
using EconomyProject.Scripts.GameEconomy;
using EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace EconomyProject.Scripts.UI.Adventurer
{

    public class AdventurerMenuManager : BaseMenuManager<AgentScreen>
    {
        public GameObject auctionMenu;

        public GameObject questMenu;

        public GameObject mainMenu;

        public GetCurrentAgent getCurrentAgent;

        public UiAccessor uiAccessor;

        public AdventurerAgent AdventurerAgent => getCurrentAgent.CurrentAgent.GetComponent<AdventurerAgent>();

        public PlayerInput PlayerInput => uiAccessor.PlayerInput;

        public override Dictionary<AgentScreen, OpenedMenu> OpenedMenus => new Dictionary<AgentScreen, OpenedMenu>
        {
            { AgentScreen.Auction, new OpenedMenu(new List<GameObject>{auctionMenu}, new List<GameObject>{questMenu, mainMenu}) },
            { AgentScreen.Main, new OpenedMenu(new List<GameObject>{mainMenu}, new List<GameObject>{questMenu, auctionMenu}) },
            { AgentScreen.Quest, new OpenedMenu(new List<GameObject>{questMenu}, new List<GameObject>{mainMenu, auctionMenu}) }
        };

        public override bool Compare(AgentScreen a, AgentScreen b)
        {
            return a == b;
        }

        private void Update()
        {
            var playerInput = PlayerInput;
            if (getCurrentAgent.CurrentAgent != null && playerInput != null)
            {
                var screen = playerInput.GetScreen(AdventurerAgent);
                
                SwitchMenu(screen);
            }
        }

        public void StartAuction()
        {
            PlayerInput.SetMainAction(AdventurerAgent, AgentScreen.Auction);
        }

        public void StartQuest()
        {
            PlayerInput.SetMainAction(AdventurerAgent, AgentScreen.Quest);
        }

        public void MainMenu()
        {
            PlayerInput.SetMainAction(AdventurerAgent, AgentScreen.Main);
        }

        public void Bid()
        {
            PlayerInput.SetAuctionChoice(AdventurerAgent, AuctionChoice.Bid);
        }
    }
}
