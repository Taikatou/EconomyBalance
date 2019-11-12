using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using Assets.EconomyProject.Scripts.UI.ShopUI.Buttons;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Buttons
{
    public class CraftingMakeRequestButton : SampleButton<CraftingResources>
    {
        public Text nameLabel;
        public Image iconImage;

        public override void SetupButton()
        {
            nameLabel.text = itemDetails.ToString();
        }
    }
}
