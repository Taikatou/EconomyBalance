using System.Collections.Generic;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman
{
    public enum CraftingChoice { Abstain, BeginnerSword, IntermediateSword, AdvancedSword, EpicSword, UltimateSwordOfPower}
    public class CraftingAbility : MonoBehaviour
    {
        public float timeToCreation = 3;

        private float _updateTime = 0.0f;

        private bool _crafting;

        private Dictionary<Resources, int> _numResources;

        private void Start()
        {
            _numResources = new Dictionary<Resources, int>();
        }

        public void AddResource(Resources resource, int count)
        {
            bool hasResource = _numResources.ContainsKey(resource);
            if (!hasResource)
            {
                _numResources[resource] = 0;
            }

            _numResources[resource] += count;
        }

        private void Update()
        {
            if (_crafting)
            {
                _updateTime += Time.deltaTime;
                if (_updateTime >= timeToCreation)
                {
                    FinishCrafting();
                }
            }
        }

        public void FinishCrafting()
        {

        }

        public void SetCraftingItem(CraftingChoice choice)
        {
            if (choice != CraftingChoice.Abstain)
            {
                _crafting = true;
            }
        }

        private void CraftItem()
        {
            _crafting = false;
        }
    }
}
