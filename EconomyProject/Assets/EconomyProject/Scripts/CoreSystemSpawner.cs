﻿using UnityEngine;

namespace Assets.EconomyProject.Scripts
{
    public class CoreSystemSpawner : MonoBehaviour
    {
        public GameObject coreSystemPrefab;

        public int numCoreSpawn;

        public bool spawnCoreSystems;
        private void Start()
        {
            if (spawnCoreSystems)
            {
                for (int i = 0; i < numCoreSpawn; i++)
                {
                    GameObject agentPrefab = Instantiate(coreSystemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    agentPrefab.transform.parent = gameObject.transform;
                }
            }
        }
    }
}
