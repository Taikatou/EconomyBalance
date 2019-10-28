using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class UiAccessor : MonoBehaviour
    {
        public int Index { get; set; }

        public GameObject coreGameSystem;

        public bool printObservation = true;

        public AdventurerAgent[] GetAgents => coreGameSystem.GetComponentsInChildren<AdventurerAgent>();

        public GameQuests GameQuests => coreGameSystem.GetComponentInChildren<GameQuests>();

        public PlayerInput PlayerInput => coreGameSystem.GetComponentInChildren<PlayerInput>();

        public GameAuction GameAuction => coreGameSystem.GetComponentInChildren<GameAuction>();

        public AdventurerAgent AdventurerAgent
        {
            get
            {
                if (GetAgents.Length > Index)
                {
                    return GetAgents[Index];
                }
                return null;
            }
        }

        private void Update()
        {
            if (printObservation)
            {
                foreach (var agent in GetAgents)
                {
                    agent.printObservations = agent == AdventurerAgent;
                }
            }
        }
    }
}
