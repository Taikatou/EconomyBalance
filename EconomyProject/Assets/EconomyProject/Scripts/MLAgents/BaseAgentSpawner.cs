using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class BaseAgentSpawner : MonoBehaviour
    {
        public void SpawnAgents(GameObject learningAgentPrefab, int numLearningAgents)
        {
            for (var i = 0; i < numLearningAgents; i++)
            {
                var agentPrefab = Instantiate(learningAgentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                agentPrefab.transform.parent = gameObject.transform;
            }
        }
    }
}
