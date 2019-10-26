namespace Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public class PlayerAdventurerAgent : AdventurerAgent
    {
        public override void AgentAction(int action)
        {
            base.AgentAction(action - 1);
        }
    }
}
