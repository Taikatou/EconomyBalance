namespace Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public class PlayerAdventurerAgent : AdventurerAgent
    {
        public override void AgentAction(int mainAction, int auctionAction)
        {
            base.AgentAction(mainAction - 1, auctionAction - 1);
        }
    }
}
