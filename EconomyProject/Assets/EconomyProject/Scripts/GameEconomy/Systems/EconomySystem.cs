using System;
using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems
{
    public abstract class EconomySystem : MonoBehaviour
    {
        public abstract float Progress { get; }
        protected abstract AgentScreen ActionChoice { get; }

        public abstract bool CanMove(EconomyAgent agent);

        public PlayerInput PlayerInput => FindObjectOfType<PlayerInput>();

        public EconomyAgent[] CurrentPlayers
        {
            get
            {
                EconomyAgent[] playerAgents = FindObjectsOfType<EconomyAgent>();
                return Array.FindAll(playerAgents, element => element.ChosenScreen == ActionChoice);
            }
        }

        protected void RequestDecisions()
        {
            foreach(var agent in CurrentPlayers)
            {
                agent.RequestDecision();
            }
        }
    }
}
