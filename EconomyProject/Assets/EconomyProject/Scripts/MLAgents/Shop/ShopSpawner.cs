using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public class ShopSpawner : AgentSpawner
    {
        public GameObject learningAgentPrefab;

        public MarketPlace marketPlace;

        public int numSpawn;
        private void Start()
        {
            SpawnAgents(learningAgentPrefab, numSpawn);
        }
    }
}
