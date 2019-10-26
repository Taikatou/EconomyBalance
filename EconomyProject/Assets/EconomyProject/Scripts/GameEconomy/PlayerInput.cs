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

        public void Start()
        {
            _economyScreens = new Dictionary<AdventurerAgent, AgentScreen>();
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
            var chosenScreen = GetScreen(agent);
            switch (chosenScreen)
            {
                case AgentScreen.Auction:
                    return gameAuction.Progress;
                case AgentScreen.Quest:
                    return gameQuests.Progress;
                case AgentScreen.Main:
                    break;
            }
            return 0.0f;
        }

        public bool CanMove(AdventurerAgent agent)
        {
            var chosenScreen = GetScreen(agent);
            switch (chosenScreen)
            {
                case AgentScreen.Auction:
                    return gameAuction.CanMove(agent);
                case AgentScreen.Quest:
                    return gameQuests.CanMove(agent);
            }
            return true;
        }

        public void SetAgentAction(AdventurerAgent agent, int action)
        {
            switch (GetScreen(agent))
            {
                case AgentScreen.Auction:
                    SetAuctionChoice(agent, (AuctionChoice)action);
                    break;
                case AgentScreen.Main:
                    SetMainAction(agent, (AgentScreen)action);
                    break;
                case AgentScreen.Quest:
                    break;
            }
        }

        public void SetAuctionChoice(AdventurerAgent agent, AuctionChoice choice)
        {
            if (GetScreen(agent) == AgentScreen.Auction)
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
