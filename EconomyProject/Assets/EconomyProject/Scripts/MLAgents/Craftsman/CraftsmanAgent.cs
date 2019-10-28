using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman
{
    public class CraftsmanAgent : Agent
    {
        private CraftingAbility CraftingAbilty => GetComponent<CraftingAbility>();
        public override void AgentAction(float[] vectorAction, string textAction)
        {
            var craftAction = Mathf.FloorToInt(vectorAction[0]);
            CraftingAbilty.SetCraftingItem(craftAction);
        }

        public override void CollectObservations()
        {
            AddVectorObs(CraftingAbilty.Crafting);
            AddVectorObs(CraftingAbilty.TimeToCreation);
        }
    }
}
