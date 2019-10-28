using System;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public class EconomyWallet : MonoBehaviour
    {
        public double startMoney;

        public double Money { get; private set; }
        // Start is called before the first frame update
        void Start()
        {
            Money = startMoney;
        }

        public void EarnMoney(float amount)
        {
            if (amount > 0)
            {
                Money = Math.Round(Money + amount);
            }
        }

        public void LoseMoney(float amount)
        {
            if (amount < 0)
            {
                Money = Math.Round(Money + amount);
            }
        }

        public void SpendMoney(float amount)
        {
            if (amount > 0)
            {
                Money -= amount;
            }
        }

        public void Reset()
        {
            Money = startMoney;
        }
    }
}
