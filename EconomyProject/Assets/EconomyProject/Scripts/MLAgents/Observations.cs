using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using MLAgents;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class Observations : Agent
    {
        public string AddVectorObs(InventoryItem item, bool condition = true, float defaultObs = 0.0f)
        {
            condition = condition && item;
            var output = " Current Item";
            output += AddVectorObs(condition ? WeaponId.GetWeaponId(item.itemName) : -1, "ItemName");
            output += AddVectorObs(condition ? item.durability : defaultObs, "Durability");
            output += AddVectorObs(condition ? item.baseDurability : defaultObs, "Base Durability");
            output += AddVectorObs(condition ? item.numLootSpawns : defaultObs, "Num Loot Spawn");
            output += AddVectorObs(condition ? item.efficiency : defaultObs, "Efficiency");
            output += AddVectorObs(item && item.unBreakable, "Unbreakable", condition);

            return output;
        }

        public string AddVectorObs(float observation, string obsName)
        {
            AddVectorObs(observation);
            return " " + obsName + ": " + observation;
        }

        public string AddVectorObs(bool observation, string obsName, bool valid=true)
        {
            if (valid)
            {
                AddVectorObs(observation ? 1 : 2);
            }
            else
            {
                AddVectorObs(0);
            }

            return " " + obsName + ": " + observation;
        }
    }
}
