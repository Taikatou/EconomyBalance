using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class QuestPunisher : MonoBehaviour
    {
        public void Punish(EconomyAgent agent)
        {
            agent?.LoseMoney(5);
        }
    }
}
