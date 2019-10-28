using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using UnityEngine;
using Resources = UnityEngine.Resources;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman
{
    public enum CraftingChoice { Abstain, BeginnerSword, IntermediateSword, AdvancedSword, EpicSword, UltimateSwordOfPower}

    public class CraftingAbility : MonoBehaviour
    {
        private Dictionary<Resources, int> _numResources;

        public bool Crafting { get; private set; }

        private float _updateTime;

        public float TimeToCreation { get; private set; }

        public CraftingRequirements craftingRequirement;

        private void Start()
        {
            _numResources = new Dictionary<Resources, int>();
        }

        public void AddResource(Resources resource, int count)
        {
            var hasResource = _numResources.ContainsKey(resource);
            if (!hasResource)
            {
                _numResources[resource] = 0;
            }

            _numResources[resource] += count;
        }

        public void SetCraftingItem(CraftingChoice choice)
        {
            if (!Crafting && choice != CraftingChoice.Abstain)
            {
                Crafting = true;
            }
        }

        public void SetCraftingItem(int choice)
        {
            SetCraftingItem((CraftingChoice) choice);
        }

        public void FinishCrafting()
        {
            var generatedItem = InventoryItem.GenerateItem(craftingRequirement.resultingItem);
            Crafting = false;
        }

        private void Update()
        {
            if (Crafting)
            {
                _updateTime += Time.deltaTime;
                if (_updateTime >= TimeToCreation)
                {
                    FinishCrafting();
                }
            }
        }
    }
}
