using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public interface IObsAgent
    {
        void AddVectorObs(float observation);
        void AddVectorObs(int observation);
        void AddVectorObs(bool observation);
    }
    public class Observations : MonoBehaviour
    {
        public static string AddVectorObs(IObsAgent agent, InventoryItem item, bool condition = true, float defaultObs = 0.0f)
        {
            condition = condition && item;
            var output = " Current Item";
            output += AddVectorObs(agent, condition ? WeaponId.GetWeaponId(item.itemName) : -1, "ItemName");
            output += AddVectorObs(agent, condition ? item.durability : defaultObs, "Durability");
            output += AddVectorObs(agent, condition ? item.baseDurability : defaultObs, "Base Durability");
            output += AddVectorObs(agent, condition ? item.numLootSpawns : defaultObs, "Num Loot Spawn");
            output += AddVectorObs(agent, condition ? item.efficiency : defaultObs, "Efficiency");
            output += AddVectorObs(agent, condition, item && item.unBreakable, "Unbreakable");

            return output;
        }

        public static string AddVectorObs(IObsAgent agent, float observation, string obsName)
        {
            agent.AddVectorObs(observation);
            return " " + obsName + ": " + observation;
        }

        public static string AddVectorObs(IObsAgent agent, bool valid, bool observation, string obsName)
        {
            if (valid)
            {
                agent.AddVectorObs(observation ? 1 : 2);
            }
            else
            {
                agent.AddVectorObs(0);
            }

            return " " + obsName + ": " + observation;
        }
    }
}
