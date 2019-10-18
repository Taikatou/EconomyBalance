using Assets.EconomyProject.Scripts.UI;

namespace Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent
{
    public class PlayerEconomyAgent : EconomyAgent
    {
        public bool uiControl = true;

        public MainMenu mainMenu;

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            if (!uiControl)
            {
                base.AgentAction(vectorAction, textAction);
            }
        }

        public override void OnSwitch()
        {
            mainMenu.SwitchMenu(ChosenScreen);
        }
    }
}
