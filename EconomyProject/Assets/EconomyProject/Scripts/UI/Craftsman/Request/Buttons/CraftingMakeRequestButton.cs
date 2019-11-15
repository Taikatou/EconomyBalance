using Assets.EconomyProject.Scripts.UI.Craftsman.Request.ScrollList;
using Assets.EconomyProject.Scripts.UI.ShopUI.Buttons;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Request.Buttons
{
    public class CraftingMakeRequestButton : SampleButton<CraftingResourceUi>
    {
        public Text nameLabel;
        public Image iconImage;
        public Text numberText;

        public override void SetupButton()
        {
            nameLabel.text = itemDetails.resourceType.ToString();
            numberText.text = "INVENTORY: " + itemDetails.inventoryNumber;
        }
    }
}
