using System;
using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class PlayerInput : MonoBehaviour
    {
        private Dictionary<EconomyAgent, AgentScreen> _economyScreens;

        private GameAuction gameAuction => FindObjectOfType<GameAuction>();

        private GameQuests gameQuests => FindObjectOfType<GameQuests>();

        public void Start()
        {
            _economyScreens = new Dictionary<EconomyAgent, AgentScreen>();
        }

        public AgentScreen GetScreen(EconomyAgent agent)
        {
            if (!_economyScreens.ContainsKey(agent))
            {
                _economyScreens.Add(agent, AgentScreen.Main);
            }
            return _economyScreens[agent];
        }

        public float GetProgress(EconomyAgent agent)
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

        public bool CanMove(EconomyAgent agent)
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

        public void SetAgentAction(EconomyAgent agent, int action)
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

        public void SetAuctionChoice(EconomyAgent agent, AuctionChoice choice)
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

        public void SetMainAction(EconomyAgent agent, AgentScreen choice)
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
