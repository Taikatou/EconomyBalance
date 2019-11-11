using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using MLAgents;
using UnityEngine;
using ResourceRequest = Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests.ResourceRequest;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman
{
    public class CraftsmanAgent : Agent
    {
        public RequestSystem requestSystem;

        private CraftingAbility CraftingAbility => GetComponent<CraftingAbility>();

        public CraftingInventory CraftingInventory => GetComponent<CraftingInventory>();

        public void AgentActionCrafting(float[] vectorAction, string textAction)
        {
            var craftAction = Mathf.FloorToInt(vectorAction[0]);
            CraftingAbility.SetCraftingItem(craftAction);

            var requestAction = Mathf.FloorToInt(vectorAction[1]);

            if (requestSystem)
            {
                CraftingResources resource = (CraftingResources)requestAction;
                requestSystem.MakeRequest(resource, CraftingInventory);
            }
        }

        public void CollectObservationsCrafting()
        {
            AddVectorObs(CraftingAbility.Crafting);
            AddVectorObs(CraftingAbility.TimeToCreation);
            var weaponId = WeaponId.GetWeaponId(CraftingAbility.RequirementName);
            AddVectorObs(weaponId);
        }

        public List<ResourceRequest> GetCraftingRequests()
        {
            return requestSystem.GetCraftingRequests(CraftingInventory);
        }
    }
}
