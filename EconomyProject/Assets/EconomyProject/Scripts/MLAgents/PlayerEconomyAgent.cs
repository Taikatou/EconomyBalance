using Assets.EconomyProject.Scripts.UI;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents
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
