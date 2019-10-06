using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI
{
    public class GeneralUi : MonoBehaviour
    {
        public Text auctionText;

        public Text damageText;

        public EconomyAgent economyAgent;

        // Update is called once per frame
        private void Update()
        {
            GameAuction gameAuction = FindObjectOfType<GameAuction>();
            if(gameAuction)
            {
                auctionText.text = "Auction Items: " + gameAuction.ItemCount.ToString();
            }
            if(economyAgent)
            {
                damageText.text = "Agent DMG: " + economyAgent.Damage;
            }
        }
    }
}
