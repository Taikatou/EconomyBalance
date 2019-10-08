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

        public Text moneyText;

        public Text durabilityText;

        public Text currentItemText;

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
                moneyText.text = "MONEY: " + economyAgent.money;

                damageText.text = "AGENT DMG: " + economyAgent.Damage;

                if(economyAgent.Item)
                {
                    durabilityText.text = "DURABILITY: " + economyAgent.Item.Durability;

                    currentItemText.text = "CURRENT ITEM: " + economyAgent.Item.Name;
                }
            }
        }
    }
}
