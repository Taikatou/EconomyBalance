using System.Globalization;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.UI.ShopUI.Buttons;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.CraftsmanCrafting
{
    public class CraftingRequestButton : SampleButton<CraftingMap>
    {
        public Text nameLabel;
        public Image iconImage;
        public Text timeToCreated;
        public override void SetupButton()
        {
            nameLabel.text = itemDetails.resource.ResultingItemName;
            timeToCreated.text = itemDetails.resource.timeToCreation.ToString(CultureInfo.InvariantCulture);
        }
    }
}
