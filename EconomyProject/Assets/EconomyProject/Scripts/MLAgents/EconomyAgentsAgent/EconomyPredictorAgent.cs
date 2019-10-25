using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent
{
    public class EconomyPredictorAgent : Agent
    {
        public override void CollectObservations()
        {
            AddVectorObs(0);
        }

        private void Update()
        {
            Time.timeScale = 100;
        }
    }
}
