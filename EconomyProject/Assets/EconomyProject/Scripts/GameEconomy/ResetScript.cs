using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class ResetScript : MonoBehaviour
    {
        public void Reset()
        {
            AdventurerAgent [] agents = GetComponentsInChildren<AdventurerAgent>();
            foreach (var agent in agents)
            {
                agent.ResetEconomyAgent();
            }

            var gameAuction = GetComponentInChildren<GameAuction>();
            gameAuction?.Reset();

            DataLogger dLogger = GetComponentInChildren<DataLogger>();
            dLogger?.Reset();
        }
    }
}
