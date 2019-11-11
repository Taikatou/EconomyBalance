using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.ShopUI.Buttons
{
    public class CraftingButton : SampleButton<ResourceRequest>
    {
        public Text nameLabel;
        public Image iconImage;
        public Text priceText;
        public Text stockText;
        public override void SetupButton()
        {
            nameLabel.text = itemDetails.Resources.ToString();
            //iconImage.sprite = _item.icon;
            stockText.text = "x" + itemDetails.Number;
        }
    }
}
