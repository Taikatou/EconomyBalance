using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class AgentSpawner : MonoBehaviour
    {
        public GameObject learningAgentPrefab;

        public int numLearningAgents = 0;

        public bool spawnAi = true;

        public GameAuction gameAuction;

        public PlayerInput playerInput;

        private void Start()
        {
            if(spawnAi)
            {
                for (int i = 0; i < numLearningAgents; i++)
                {
                    GameObject agentPrefab = Instantiate(learningAgentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    agentPrefab.transform.parent = gameObject.transform;
                }
            }
        }
    }
}
