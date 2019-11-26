using System.Globalization;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.UI.ShopUI.Buttons;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.Inventory
{
    public class InventoryScrollButton : SampleButton<InventoryItem>
    {
        public Text nameLabel;
        public Text effectiveText;
        public Image iconImage;
        public override void SetupButton()
        {
            nameLabel.text = itemDetails.itemName;
            effectiveText.text = itemDetails.efficiency.ToString(CultureInfo.InvariantCulture);
        }
    }
}
