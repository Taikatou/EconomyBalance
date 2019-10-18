using System;
using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public abstract class EconomySystem : MonoBehaviour
    {
        public abstract float Progress { get; }
        protected abstract AgentScreen actionChoice { get; }

        public EconomyAgent[] CurrentPlayers
        {
            get
            {
                EconomyAgent[] playerAgents = FindObjectsOfType<EconomyAgent>();
                return Array.FindAll(playerAgents, element => element.chosenScreen == actionChoice);
            }
        }

        protected void RequestDecision()
        {
            foreach(var agent in CurrentPlayers)
            {
                agent.RequestDecision();
            }
        }
    }
}
