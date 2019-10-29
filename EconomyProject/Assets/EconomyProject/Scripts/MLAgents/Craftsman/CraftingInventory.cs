using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman
{
    public class CraftingInventory : MonoBehaviour
    {
        private Dictionary<CraftingResources, int> _numResources;

        private void Start()
        {
            _numResources = new Dictionary<CraftingResources, int>();
        }

        public void AddResource(CraftingResources resource, int count)
        {
            var hasResource = _numResources.ContainsKey(resource);
            if (!hasResource)
            {
                _numResources[resource] = 0;
            }

            _numResources[resource] += count;
        }
    }
}
