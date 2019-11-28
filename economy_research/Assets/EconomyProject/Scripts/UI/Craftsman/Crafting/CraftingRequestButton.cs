using System.Globalization;
using EconomyProject.Scripts.MLAgents.Craftsman;
using EconomyProject.Scripts.UI.ShopUI.Buttons;
using UnityEngine.UI;

namespace EconomyProject.Scripts.UI.Craftsman.Crafting
{
    public class CraftingInfo
    {
        public CraftingMap craftingMap;
        public CraftingInventory craftingInventory;

        public CraftingInfo(CraftingMap craftingMap, CraftingInventory craftingInventory)
        {
            this.craftingMap = craftingMap;
            this.craftingInventory = craftingInventory;
        }
    }
    public class CraftingRequestButton : SampleButton<CraftingInfo>
    {
        public Text nameLabel;
        public Image iconImage;
        public Text timeToCreated;
        public Text specsText;
        public override void SetupButton()
        {
            nameLabel.text = itemDetails.craftingMap.resource.ResultingItemName;
            timeToCreated.text = itemDetails.craftingMap.resource.timeToCreation.ToString(CultureInfo.InvariantCulture);
            var outputText = "";
            foreach (var requirement in itemDetails.craftingMap.resource.resourcesRequirements)
            {
                var currentInventory = itemDetails.craftingInventory.GetResourceNumber(requirement.type);
                outputText += requirement.type + "\t" + requirement.number + "/" + currentInventory + "\n";
            }

            specsText.text = outputText;
        }
    }
}
