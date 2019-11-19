using System;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using MLAgents;
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
                if (agent.agentParameters.onDemandDecision)
                {
                    agent.RequestDecision();
                }
            }
        }

        // public abstract void SetChoice(CurrentAgent agent, int input1, int input2);
    }
}
