using System.Globalization;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.ShopUI
{
    public class SampleButton : MonoBehaviour
    {
        public Button buttonComponent;
        public Text nameLabel;
        public Image iconImage;
        public Text priceText;

        private ShopItem _itemDetails;
        private ShopScrollList _scrollList;

        // Use this for initialization
        private void Start()
        {
            buttonComponent.onClick.AddListener(HandleClick);
        }

        public void Setup(ShopItem itemDetails, ShopScrollList currentScrollList)
        {
            _itemDetails = itemDetails;
            nameLabel.text = _itemDetails.ItemName;
            //iconImage.sprite = _item.icon;
            priceText.text = itemDetails.price.ToString(CultureInfo.InvariantCulture);
            _scrollList = currentScrollList;
        }

        public void HandleClick()
        {
            var number = 1;
            _scrollList.TryTransferItemToOtherShop(_itemDetails, number);
        }
    }
}