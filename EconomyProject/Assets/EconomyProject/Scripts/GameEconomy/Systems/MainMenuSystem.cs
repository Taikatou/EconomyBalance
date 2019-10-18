using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems
{
    public class MainMenuSystem : EconomySystem
    {
        public override float Progress => throw new System.NotImplementedException();

        protected override AgentScreen ActionChoice => AgentScreen.Main;
        public override bool CanMove(EconomyAgent agent)
        {
            return true;
        }

        private void Update()
        {
            RequestDecisions();
        }
    }
}
