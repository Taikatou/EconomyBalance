using Assets.EconomyProject.Scripts.GameEconomy;
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
            if (gameAuction.ItemCount > 0)
            {
                slider.value = Progress;
                itemText.text = "Name: " + gameAuction.auctionedItem.Name +
                                " Dmg" + gameAuction.auctionedItem.dmg +
                                " Bid price: " + gameAuction.auctionedItem.baseBidPrice;
            }
            else
            {
                itemText.text = "";
            }
        }

        public float Progress => gameAuction.Progress;
    }
}
