using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class QuestPunisher : MonoBehaviour
    {
        public void Punish(AdventurerAgent agent)
        {
            agent?.LoseMoney(5);
        }
    }
}
