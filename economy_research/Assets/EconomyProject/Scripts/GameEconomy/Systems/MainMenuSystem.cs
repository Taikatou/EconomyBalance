using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems
{
    public class MainMenuSystem : EconomySystem
    {
        public override float Progress => 0;
        protected override AgentScreen ActionChoice => AgentScreen.Main;
        public override bool CanMove(AdventurerAgent agent)
        {
            return true;
        }

        private void Update()
        {
            RequestDecisions();
        }
    }
}
