using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.Craftsman
{
    public class CraftsmanGetAgent : GetCurrentAgent<CraftsmanAgent>
    {
        public GameObject agentParent;
        public override GameObject AgentParent => agentParent;

        public void MoveToRequest()
        {
            CurrentAgent.ChangeAgentScreen(CraftsmanScreen.Request);
        }

        public void MoveToCraft()
        {
            CurrentAgent.ChangeAgentScreen(CraftsmanScreen.Craft);
        }

        public void ReturnToMain()
        {
            CurrentAgent.ChangeAgentScreen(CraftsmanScreen.Main);
        }
    }
}
