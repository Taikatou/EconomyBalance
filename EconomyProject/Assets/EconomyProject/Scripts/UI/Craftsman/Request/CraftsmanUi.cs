using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Request
{
    public class CraftsmanUi : MonoBehaviour
    {
        public Text requestText;

        public RequestSystem requestSystem;
        public CraftsmanAgent CraftsmanAgent => GetComponentInParent<CraftsmanMenu>().CurrentAgent;
        void Update()
        {
            var number = requestSystem.GetRequestNumber(CraftsmanAgent.CraftingInventory);
            requestText.text = number.ToString();
        }
    }
}
