using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents;

namespace Assets.EconomyProject.Scripts.GameEconomy
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
