using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class AgentSpawner : MonoBehaviour
    {
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
