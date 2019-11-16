using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using MLAgents;
using UnityEngine;
using ResourceRequest = Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests.ResourceRequest;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman
{
    public enum CraftsmanScreen { Main, Request, Craft }

    public class CraftsmanAgent : Agent
    {
        public RequestSystem requestSystem;
        public CraftsmanScreen CurrentScreen { get; private set; }
        public CraftingAbility CraftingAbility => GetComponent<CraftingAbility>();
        public CraftingInventory CraftingInventory => GetComponent<CraftingInventory>();
        public AgentInventory AgentInventory => GetComponent<AgentInventory>();

        public void AgentActionCrafting(float[] vectorAction, string textAction)
        {
            var craftAction = Mathf.FloorToInt(vectorAction[0]);
            CraftingAbility.SetCraftingItem(craftAction);

            var requestAction = Mathf.FloorToInt(vectorAction[1]);

            if (requestSystem)
            {
                var resource = (CraftingResources)requestAction;
                MakeRequest(resource);
            }
        }

        public void MakeRequest(CraftingResources resource)
        {
            requestSystem.MakeRequest(resource, CraftingInventory);
        }

        public void CollectObservationsCrafting()
        {
            AddVectorObs(CraftingAbility.Crafting);
            AddVectorObs(CraftingAbility.TimeToCreation);
            AddVectorObs((int)CurrentScreen);

            var weaponId = WeaponId.GetWeaponId(CraftingAbility.ChosenCrafting.itemName);
            AddVectorObs(weaponId);
        }

        public void ChangeAgentScreen(CraftsmanScreen nextScreen)
        {
            CurrentScreen = nextScreen;
        }

        public List<ResourceRequest> GetCraftingRequests()
        {
            return requestSystem.GetCraftingRequests(CraftingInventory);
        }
    }
}
