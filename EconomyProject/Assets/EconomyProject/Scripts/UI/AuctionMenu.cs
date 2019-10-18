using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI
{
    public class AuctionMenu : MonoBehaviour
    {
        public Slider slider;

        public Text itemText;

        private GameAuction gameAuction => FindObjectOfType<GameAuction>();

        private void Update()
        {
            if (gameAuction.ItemCount > 0 && gameAuction.auctionedItem)
            {
                slider.value = Progress;
                itemText.text = "Name: " + gameAuction.auctionedItem.Name +
                                " efficiency" + gameAuction.auctionedItem.efficiency +
                                " Bid price: " + gameAuction.currentItemPrice;
            }
            else
            {
                itemText.text = "";
            }
        }

        public float Progress => gameAuction.Progress;
    }
}
