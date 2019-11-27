using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEditor;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.Experiments
{
    public class ResetOnItem : MonoBehaviour
    {
        public InventoryItem endItem;

        public bool resetOnComplete = false;

        public GameObject agentList;

        public AdventurerAgent [] Agents => agentList?.GetComponentsInChildren<AdventurerAgent>();

        private void Update()
        {
            if (resetOnComplete && endItem)
            {
                foreach (var agent in Agents)
                {
                    var hasEndItem = agent.inventory.ContainsItem(endItem);
                    if (hasEndItem)
                    {
                        Debug.Log("Complete");
                        agent.AddReward(1);
                        agent.Done();
                    }
                }
            }
        }
    }
}
