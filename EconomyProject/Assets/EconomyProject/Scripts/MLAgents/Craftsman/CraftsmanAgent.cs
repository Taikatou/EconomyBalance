using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman
{
    public class CraftsmanAgent : Agent
    {
        private CraftingAbility CraftingAbility => GetComponent<CraftingAbility>();
        public override void AgentAction(float[] vectorAction, string textAction)
        {
            var craftAction = Mathf.FloorToInt(vectorAction[0]);
            CraftingAbility.SetCraftingItem(craftAction);

            var requestAction = Mathf.FloorToInt(vectorAction[1]);
            var requestSystem = FindObjectOfType<RequestSystem>();

            if (requestSystem)
            {
                CraftingResources resource = (CraftingResources)requestAction;
                requestSystem.MakeRequest(resource, GetComponent<CraftingInventory>());
            }
        }

        public override void CollectObservations()
        {
            AddVectorObs(CraftingAbility.Crafting);
            AddVectorObs(CraftingAbility.TimeToCreation);
        }
    }
}
