using System.Globalization;
using EconomyProject.Scripts.Inventory;
using EconomyProject.Scripts.UI.ShopUI.Buttons;
using UnityEngine.UI;

namespace EconomyProject.Scripts.UI.Inventory
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
