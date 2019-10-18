﻿using System;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent
{
    public class EconomyWallet : MonoBehaviour
    {
        public double startMoney;

        private double _money;

        private GameQuests GameQuests => FindObjectOfType<GameQuests>();

        public double Money => _money;
        // Start is called before the first frame update
        void Start()
        {
            _money = startMoney;
        }

        public void EarnMoney(float amount)
        {
            if (amount > 0)
            {
                _money = Math.Round(_money + amount);
                var reward = _money / GameQuests.MaxMoney;
                GetComponent<EconomyAgent>()?.AddReward((float)reward);
            }
        }

        public void SpendMoney(float amount)
        {
            if (amount > 0)
            {
                _money -= amount;
            }
        }

        public void Reset()
        {
            _money = startMoney;
        }
    }
}
