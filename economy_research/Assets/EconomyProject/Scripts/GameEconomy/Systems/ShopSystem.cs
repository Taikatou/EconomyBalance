using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems
{
    public class ShopSystem : EconomySystem
    {
        public override float Progress => 0.0f;
        protected override AgentScreen ActionChoice => AgentScreen.Shop;
        public override bool CanMove(AdventurerAgent agent)
        {
            return true;
        }
    }
}
