using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.Experiments
{
    public class ResetOnItem : MonoBehaviour
    {
        public InventoryItem endItem;

        public bool resetOnComplete = false;

        public AdventurerAgent completeAgent;

        public bool addReward;

        public void CheckItem(InventoryItem item)
        {
            if (resetOnComplete && item && endItem)
            {
                if (item.itemName == endItem.itemName)
                {
                    if (addReward)
                    {
                        var reward = item.efficiency / endItem.efficiency;
                        completeAgent.AddReward(reward);
                    }
                    completeAgent.Done();
                }
            }
        }
    }
}
