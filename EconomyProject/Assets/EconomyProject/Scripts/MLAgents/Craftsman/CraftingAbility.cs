using System.Collections.Generic;
using System.Linq;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman
{
    [System.Serializable]
    public struct CraftingMap
    {
        public CraftingChoice choice;
        public CraftingRequirements resource;
    }

    public enum CraftingChoice { Abstain, BeginnerSword, IntermediateSword, AdvancedSword, EpicSword, UltimateSwordOfPower}

    public class CraftingAbility : MonoBehaviour
    {
        public bool Crafting { get; private set; }

        private float _updateTime;

        public float TimeToCreation { get; private set; }

        public List<CraftingMap> craftingRequirement;

        private CraftingRequirements _chosenRequirements;

        public string RequirementName => _chosenRequirements.resultingItem.itemName;

        public void SetCraftingItem(CraftingChoice choice)
        {
            var foundChoice = craftingRequirement.Single(c => c.choice == choice).resource;
            if (!Crafting && foundChoice)
            {
                Crafting = true;
                _chosenRequirements = foundChoice;
            }
        }

        public void SetCraftingItem(int choice)
        {
            SetCraftingItem((CraftingChoice) choice);
        }

        public void FinishCrafting()
        {
            var generatedItem = InventoryItem.GenerateItem(_chosenRequirements.resultingItem);
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
