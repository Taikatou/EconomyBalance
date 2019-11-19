using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman
{
    public class CraftsmanAgentSpawner : AgentSpawner
    {
        public RequestSystem requestSystem;
        public override GameObject Spawn(GameObject toSpawnPrefab)
        {
            var agent = base.Spawn(toSpawnPrefab);
            var craftsman = agent.GetComponent<CraftsmanAgent>();
            craftsman?.SetRequestSystem(requestSystem);
            return agent;
        }
    }
}
