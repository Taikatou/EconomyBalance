using EconomyProject.Scripts.MLAgents.Craftsman;
using EconomyProject.Scripts.MLAgents.Shop;
using UnityEngine;

namespace EconomyProject.Scripts.UI.Craftsman
{
    public class CraftsmanUIControls : MonoBehaviour
    {
        public GetCurrentAgent getCurrentAgent;

        public ShopAgent CraftsmanAgent => getCurrentAgent.CurrentAgent.GetComponent<ShopAgent>();
        public void MoveToRequest()
        {
            CraftsmanAgent?.ChangeAgentScreen(CraftsmanScreen.Request);
        }

        public void MoveToCraft()
        {
            CraftsmanAgent?.ChangeAgentScreen(CraftsmanScreen.Craft);
        }

        public void ReturnToMain()
        {
            CraftsmanAgent?.ChangeAgentScreen(CraftsmanScreen.Main);
        }
    }
}
