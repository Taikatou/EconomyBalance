using System;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems
{
    public abstract class EconomySystem : MonoBehaviour
    {
        public abstract float Progress { get; }
        protected abstract AgentScreen ActionChoice { get; }

        public abstract bool CanMove(AdventurerAgent agent);

        public PlayerInput playerInput;

        public GameObject agents;

        public AdventurerAgent[] CurrentPlayers
        {
            get
            {
                AdventurerAgent[] playerAgents = agents.GetComponentsInChildren<AdventurerAgent>();
                return Array.FindAll(playerAgents, element => element.ChosenScreen == ActionChoice);
            }
        }

        protected virtual void RequestDecisions()
        {
            foreach(var agent in CurrentPlayers)
            {
                agent.RequestDecision();
            }
        }
    }
}
