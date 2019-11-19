using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class BaseAgentSpawner : MonoBehaviour
    {
        public void SpawnAgents(GameObject learningAgentPrefab, int numLearningAgents)
        {
            for (var i = 0; i < numLearningAgents; i++)
            {
                var agentPrefab = Spawn(learningAgentPrefab);
                agentPrefab.transform.parent = gameObject.transform;
            }
        }

        public virtual GameObject Spawn(GameObject toSpawnPrefab)
        {
            var agentPrefab = Instantiate(toSpawnPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            return agentPrefab;
        }
    }
}
