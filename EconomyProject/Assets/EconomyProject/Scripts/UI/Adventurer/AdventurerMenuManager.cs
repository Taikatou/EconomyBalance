﻿using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.Adventurer
{
    public class AdventurerMenuManager : BaseMenuManager<AgentScreen>
    {
        public GameObject auctionMenu;

        public GameObject questMenu;

        public GameObject mainMenu;

        public AdventurerGetAgent accessor;

        public PlayerInput PlayerInput => GetComponent<UiAccessor>().PlayerInput;

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
            if (accessor.CurrentAgent != null && playerInput != null)
            {
                var screen = playerInput.GetScreen(accessor.CurrentAgent);
                
                SwitchMenu(screen);
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
