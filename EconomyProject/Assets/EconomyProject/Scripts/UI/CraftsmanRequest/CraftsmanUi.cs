using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.CraftsmanRequest
{
    public class CraftsmanUi : MonoBehaviour
    {
        public Text requestText;
        public RequestSystem requestSystem;
        public CraftsmanAgent craftsmanAgent;
        void Update()
        {
            var number = requestSystem.GetRequestNumber(craftsmanAgent.CraftingInventory);
            requestText.text = number.ToString();
        }
    }
}
