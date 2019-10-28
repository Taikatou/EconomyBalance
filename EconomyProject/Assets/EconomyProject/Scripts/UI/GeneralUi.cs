using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI
{
    public class GeneralUi : MonoBehaviour
    {
        public Text auctionText;

        public Text moneyText;

        public Text durabilityText;

        public Text currentItemText;

        public Text efficiencyText;

        public UiAccessor accessor;

        // Update is called once per frame
        private void Update()
        {
            AdventurerAgent adventurerAgent = accessor.AdventurerAgent;
            GameAuction gameAuction = accessor.GameAuction;
            if (gameAuction)
            {
                auctionText.text = "Auction Items: " + gameAuction.ItemCount.ToString();
            }
            if(adventurerAgent)
            {
                moneyText.text = "MONEY: " + adventurerAgent.Wallet.Money;

                if(adventurerAgent.Item)
                {
                    durabilityText.text = "DURABILITY: " + (adventurerAgent.Item.unBreakable? "∞" : adventurerAgent.Item.durability.ToString());

                    currentItemText.text = "CURRENT ITEM: " + adventurerAgent.Item.itemName;

                    efficiencyText.text = "EFFICIENCY: " + adventurerAgent.Item.efficiency;
                }
            }
        }
    }
}
