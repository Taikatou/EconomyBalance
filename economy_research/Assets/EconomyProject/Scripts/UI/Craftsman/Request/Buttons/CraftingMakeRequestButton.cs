using EconomyProject.Scripts.UI.Craftsman.Request.ScrollList;
using EconomyProject.Scripts.UI.ShopUI.Buttons;
using UnityEngine.UI;

namespace EconomyProject.Scripts.UI.Craftsman.Request.Buttons
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
