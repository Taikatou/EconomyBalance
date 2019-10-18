﻿using Assets.EconomyProject.Scripts.GameEconomy;
using MLAgents;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class EconomyAcademy : Academy
    {
        private static GameAuction GameAuction => FindObjectOfType<GameAuction>();

        private static GameQuests GameQuests => FindObjectOfType<GameQuests>();

        public override void InitializeAcademy()
        {
            Monitor.SetActive(true);
        }

        public override void AcademyReset()
        {
            GameAuction.Reset();
        }
    }
}
