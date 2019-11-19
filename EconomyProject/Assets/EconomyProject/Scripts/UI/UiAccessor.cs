using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class UiAccessor : GetCurrentAgent<AdventurerAgent>
    {
        public GameObject coreGameSystem;

        public bool printObservation = true;

        public override GameObject AgentParent => coreGameSystem;

        public GameQuests GameQuests => coreGameSystem.GetComponentInChildren<GameQuests>();

        public PlayerInput PlayerInput => coreGameSystem.GetComponentInChildren<PlayerInput>();

        public GameAuction GameAuction => coreGameSystem.GetComponentInChildren<GameAuction>();

        private void Update()
        {
            if (printObservation)
            {
                foreach (var agent in GetAgents)
                {
                    agent.printObservations = agent == CurrentAgent;
                }
            }
        }
    }
}
