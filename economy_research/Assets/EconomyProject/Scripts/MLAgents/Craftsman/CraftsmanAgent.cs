using System.Collections.Generic;
using EconomyProject.Scripts.GameEconomy.Systems.Requests;
using EconomyProject.Scripts.Inventory;
using EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using UnityEngine;
using ResourceRequest = EconomyProject.Scripts.GameEconomy.Systems.Requests.ResourceRequest;

namespace EconomyProject.Scripts.MLAgents.Craftsman
{
    public enum CraftsmanScreen { Stay, Main, Request, Craft }

    public class CraftsmanAgentG : MonoBehaviour
    {
        public RequestSystem requestSystem;
        public CraftsmanScreen CurrentScreen { get; private set; }
        public CraftingAbility CraftingAbility => GetComponent<CraftingAbility>();
        public CraftingInventory CraftingInventory => GetComponent<CraftingInventory>();
        public AgentInventory AgentInventory => GetComponent<AgentInventory>();

        public void SetRequestSystem(RequestSystem newRequestSystem)
        {
            requestSystem = newRequestSystem;
        }

        public void AgentActionCrafting(float[] vectorAction, string textAction)
        {
            /*var screenAction = Mathf.FloorToInt(vectorAction[0]);
            var nextScreen = (CraftsmanScreen) screenAction;
            if (nextScreen != CraftsmanScreen.Stay)
            {
                ChangeAgentScreen(nextScreen);
            }

            var craftAction = Mathf.FloorToInt(vectorAction[1]);
            CraftingAbility.SetCraftingItem(craftAction);

            var requestAction = Mathf.FloorToInt(vectorAction[2]);

            if (requestSystem)
            {
                var resource = (CraftingResources)requestAction;
                MakeRequest(resource);
            }*/
        }

        public void MakeRequest(CraftingResources resource)
        {
            requestSystem.MakeRequest(resource, CraftingInventory);
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
