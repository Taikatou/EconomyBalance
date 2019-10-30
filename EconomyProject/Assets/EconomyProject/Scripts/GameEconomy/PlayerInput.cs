using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class PlayerInput : MonoBehaviour
    {
        private Dictionary<AdventurerAgent, AgentScreen> _economyScreens;

        public GameAuction gameAuction;

        public GameQuests gameQuests;

        public MainMenuSystem mainMenuSystem;

        public int NumberInAuction => gameAuction.CurrentPlayers.Length;

        public int NumberInQuest => gameQuests.CurrentPlayers.Length;

        public void Start()
        {
            _economyScreens = new Dictionary<AdventurerAgent, AgentScreen>();
        }

        public EconomySystem GetEconomySystem(AdventurerAgent agent)
        {
            switch (GetScreen(agent))
            {
                case AgentScreen.Main:
                    return mainMenuSystem;
                case AgentScreen.Quest:
                    return gameQuests;
                case AgentScreen.Auction:
                    return gameAuction;
            }
            return null;
        }

        public AgentScreen GetScreen(AdventurerAgent agent)
        {
            if (agent)
            {
                if (!_economyScreens.ContainsKey(agent))
                {
                    _economyScreens.Add(agent, AgentScreen.Main);
                }
                return _economyScreens[agent];
            }

            return (AgentScreen)(-1);
        }

        public float GetProgress(AdventurerAgent agent)
        {
            var system = GetEconomySystem(agent);
            return system.Progress;
        }

        public bool CanMove(AdventurerAgent agent)
        {
            var system = GetEconomySystem(agent);
            return system.CanMove(agent);
        }

        public void SetAgentAction(AdventurerAgent agent, int mainAction, int auctionAction)
        {
            switch (GetScreen(agent))
            {
                case AgentScreen.Auction:
                    SetAuctionChoice(agent, auctionAction);
                        break;
                case AgentScreen.Main:
                    SetMainAction(agent, mainAction);
                    break;
                case AgentScreen.Quest:
                    break;
            }
        }

        public void SetAuctionChoice(AdventurerAgent agent, int choice)
        {
            if (choice >= 0)
            {
                var action = (AuctionChoice) choice;
                SetAuctionChoice(agent, action);
            }
        }

        public void SetAuctionChoice(AdventurerAgent agent, AuctionChoice choice)
        {
            switch (choice)
            {
                case AuctionChoice.Ignore:
                    break;
                case AuctionChoice.Bid:
                    gameAuction.Bid(agent);
                    break;
            }
        }

        public void SetMainAction(AdventurerAgent agent, int choice)
        {
            if (choice >= 0)
            {
                AgentScreen action = (AgentScreen)choice;
                SetMainAction(agent, action);
            }
        }

        public void SetMainAction(AdventurerAgent agent, AgentScreen choice)
        {
            var canChange = CanMove(agent);
            if (canChange)
            {
                _economyScreens[agent] = choice;
            }
        }

        public void Reset()
        {
            _economyScreens.Clear();
        }
    }
}
