namespace Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent
{
    public class PlayerEconomyAgent : EconomyAgent
    {
        public bool uiControl = true;

        public override void AgentAction(int action)
        {
            base.AgentAction(action - 1);
        }
    }
}
