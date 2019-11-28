﻿using EconomyProject.Scripts.GameEconomy.Systems.Requests;
using EconomyProject.Scripts.UI.ShopUI.Buttons;
using UnityEngine.UI;

namespace EconomyProject.Scripts.UI.Craftsman.Request.Buttons
{
    public class CraftingCurrentRequestButton : SampleButton<ResourceRequest>
    {
        public Text nameLabel;
        public Image iconImage;
        public Text stockText;
        public override void SetupButton()
        {
            nameLabel.text = itemDetails.Resources.ToString();
            //iconImage.sprite = _item.icon;
            stockText.text = "x" + itemDetails.Number;
        }
    }
}
