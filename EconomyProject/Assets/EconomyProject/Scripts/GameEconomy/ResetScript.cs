using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class ResetScript : MonoBehaviour
    {
        public void Reset()
        {
            EconomyAgent [] agents = GetComponentsInChildren<EconomyAgent>();
            foreach (var agent in agents)
            {
                agent.ResetEconomyAgent();
            }

            var gameAuction = GetComponentInChildren<GameAuction>();
            gameAuction.Reset();
        }
    }
}
