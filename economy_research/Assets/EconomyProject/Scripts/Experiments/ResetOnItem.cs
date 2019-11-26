using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.Experiments
{
    public class ResetOnItem : MonoBehaviour
    {
        public InventoryItem endItem;

        public bool resetOnComplete = false;

        public AdventurerAgent [] Agents => GetComponentsInChildren<AdventurerAgent>();

        private void Update()
        {
            if (resetOnComplete && endItem)
            {
                foreach (var agent in Agents)
                {
                    var hasEndItem = agent.inventory.ContainsItem(endItem);
                    if (hasEndItem)
                    {
                        agent.Done();
                    }
                }
            }
        }
    }
}
