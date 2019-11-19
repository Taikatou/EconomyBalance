using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Request
{
    public class CraftsmanUi : MonoBehaviour
    {
        public Text requestText;

        public RequestSystem requestSystem;
        public CraftsmanUIControls CraftsmanUiControls => GetComponentInParent<CraftsmanUIControls>();
        public ShopAgent CraftsmanAgent => CraftsmanUiControls.CraftsmanAgent;
        void Update()
        {
            var number = requestSystem.GetRequestNumber(CraftsmanAgent.CraftingInventory);
            requestText.text = number.ToString();
        }
    }
}
