﻿using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts
{
    public class CoreSystemSpawner : MonoBehaviour
    {
        public GameObject coreSystemPrefab;

        public int numCoreSpawn;

        public bool spawnCoreSystems;

        public GameObject learningAgentPrefab;

        public GameObject playerAgentPrefab;

        public int numLearningAgents;

        public bool spawnPlayer;

        private void Start()
        {
            if (spawnCoreSystems)
            {
                for (var i = 0; i < numCoreSpawn; i++)
                {
                    var agentPrefab = Instantiate(coreSystemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    agentPrefab.transform.parent = gameObject.transform;

                    var spawner = GetComponentInChildren<AgentSpawner>();
                    var adventurerPrefab = spawnPlayer ? playerAgentPrefab : learningAgentPrefab;
                    spawner?.SpawnAgents(adventurerPrefab, numLearningAgents);
                }
            }
        }
    }
}
