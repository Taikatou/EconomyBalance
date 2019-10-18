namespace Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent
{
    public class PlayerEconomyAgent : EconomyAgent
    {
        public bool uiControl = true;

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            if (!uiControl)
            {
                base.AgentAction(vectorAction, textAction);
            }
        }
    }
}
