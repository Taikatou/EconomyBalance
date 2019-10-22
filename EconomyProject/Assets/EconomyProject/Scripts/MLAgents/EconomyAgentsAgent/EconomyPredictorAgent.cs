using MLAgents;

namespace Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent
{
    public class EconomyPredictorAgent : Agent
    {
        public override void CollectObservations()
        {
            AddVectorObs(0);
        }
    }
}
