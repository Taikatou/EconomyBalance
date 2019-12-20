using EconomyProject.Scripts.MLAgents.AdventurerAgents;

namespace EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public class PlayerAdventurerAgent : AdventurerAgent
    {
        protected override void AgentAction(int mainAction, int auctionAction)
        {
            base.AgentAction(mainAction - 1, auctionAction - 1);
        }
    }
}
