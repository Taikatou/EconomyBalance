using MLAgents;

namespace Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public class EconomyPredictorAgent : Agent
    {
        public override void CollectObservations()
        {
            AddVectorObs(0);
        }
    }
}
