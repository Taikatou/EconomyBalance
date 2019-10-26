using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman.CraftingRequirements
{
    public enum Resources { Wood, Metal, Gem, DragonScale }

    [System.Serializable]
    public struct ResourceRequirement
    {
        public Resources type;
        public int number;
    }

    [CreateAssetMenu]
    public class CraftingRequirements : ScriptableObject
    {
        public InventoryItem resultingItem;

        public List<ResourceRequirement> resourcesRequirements = new List<ResourceRequirement>();
    }
}
