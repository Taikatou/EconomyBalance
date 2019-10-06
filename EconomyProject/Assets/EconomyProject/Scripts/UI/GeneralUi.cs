using Assets.EconomyProject.Scripts.GameEconomy;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI
{
    public class GeneralUi : MonoBehaviour
    {
        public Text textUi;

        // Update is called once per frame
        private void Update()
        {
            GameAuction gameAuction = FindObjectOfType<GameAuction>();
            textUi.text = "Auction Items: " + gameAuction.ItemCount.ToString();
        }
    }
}
