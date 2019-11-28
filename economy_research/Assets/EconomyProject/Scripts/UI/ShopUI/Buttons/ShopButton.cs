using EconomyProject.Scripts.MLAgents.Shop;
using UnityEngine.UI;

namespace EconomyProject.Scripts.UI.ShopUI.Buttons
{
    public class ShopButton : SampleButton<ShopItem>
    {
        public Text nameLabel;
        public Image iconImage;
        public Text priceText;
        public Text stockText;

        public override void SetupButton()
        {
            nameLabel.text = itemDetails.ItemName;
            //iconImage.sprite = _item.icon;
            priceText.text = itemDetails.price.ToString();
            stockText.text = "x" + itemDetails.stock;
        }
    }
}
