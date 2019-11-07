using System.Globalization;
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


        private Item _item;
        private ShopScrollList _scrollList;

        // Use this for initialization
        private void Start()
        {
            buttonComponent.onClick.AddListener(HandleClick);
        }

        public void Setup(Item currentItem, ShopScrollList currentScrollList)
        {
            _item = currentItem;
            nameLabel.text = _item.itemName;
            iconImage.sprite = _item.icon;
            priceText.text = _item.price.ToString(CultureInfo.InvariantCulture);
            _scrollList = currentScrollList;
        }

        public void HandleClick()
        {
            _scrollList.TryTransferItemToOtherShop(_item);
            Debug.Log(_item.itemId);
        }
    }
}