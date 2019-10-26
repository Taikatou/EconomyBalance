using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class AgentSpawner : MonoBehaviour
    {
        public GameAuction gameAuction;

        public PlayerInput playerInput;
        public void SpawnAgents(GameObject learningAgentPrefab, int numLearningAgents)
        {
            for (int i = 0; i < numLearningAgents; i++)
            {
                GameObject agentPrefab = Instantiate(learningAgentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                agentPrefab.transform.parent = gameObject.transform;
            }
        }
    }
}
