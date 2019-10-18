using Assets.EconomyProject.Scripts.MLAgents;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class MainMenuSystem : EconomySystem
    {
        public override float Progress => throw new System.NotImplementedException();

        protected override AgentScreen actionChoice => AgentScreen.Main;

        private void Update()
        {
            RequestDecision();
        }
    }
}
