﻿using EconomyProject.Scripts.MLAgents.AdventurerAgents;
using EconomyProject.Scripts.MLAgents.AdventurerAgents;

namespace EconomyProject.Scripts.GameEconomy.Systems
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
