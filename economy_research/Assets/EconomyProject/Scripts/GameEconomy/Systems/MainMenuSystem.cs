using EconomyProject.Scripts.MLAgents.AdventurerAgents;

namespace EconomyProject.Scripts.GameEconomy.Systems
{
    public class MainMenuSystem : EconomySystem
    {
        public GameAuction gameAuction;
        public override float Progress => 0;
        protected override AgentScreen ActionChoice => AgentScreen.Main;
        public override bool CanMove(AdventurerAgent agent)
        {
            return true;
        }

        private void Update()
        {
            if (gameAuction.ItemCount > 0)
            {
                RequestDecisions();
            }
            else
            {
                foreach (var agent in CurrentPlayers)
                {
                    playerInput.SetMainAction(agent, AgentScreen.Quest);
                }
            }
        }
    }
}
