using MLAgents;

namespace EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public class EconomyPredictorAgent : Agent
    {
        public override void CollectObservations()
        {
            AddVectorObs(0);
        }
    }
}
