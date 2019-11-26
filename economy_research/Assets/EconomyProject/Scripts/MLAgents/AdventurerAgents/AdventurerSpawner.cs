using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public class AdventurerSpawner : BaseAgentSpawner
    {
        public GameAuction gameAuction;

        public PlayerInput playerInput;

        public override GameObject Spawn(GameObject toSpawnPrefab)
        {
            var agent = base.Spawn(toSpawnPrefab);
            var adventurer = agent.GetComponent<AdventurerAgent>();
            adventurer.gameAuction = gameAuction;
            adventurer.playerInput = playerInput;
            return agent;
        }
    }
}
