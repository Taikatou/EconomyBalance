using System;
using EconomyProject.Scripts.MLAgents.AdventurerAgents;
using EconomyProject.Scripts.MLAgents.AdventurerAgents;
using MLAgents;
using UnityEngine;

namespace EconomyProject.Scripts.GameEconomy.Systems
{
    public abstract class EconomySystem : MonoBehaviour
    {
        public PlayerInput playerInput;

        public GameObject agents;

        public abstract float Progress { get; }
        protected abstract AgentScreen ActionChoice { get; }

        public abstract bool CanMove(AdventurerAgent agent);

        public AdventurerAgent[] CurrentPlayers
        {
            get
            {
                var playerAgents = agents.GetComponentsInChildren<AdventurerAgent>();
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
