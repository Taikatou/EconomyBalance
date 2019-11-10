using System;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.ShopUI
{
    public class UpdateItemUI : MonoBehaviour
    {
        public Button moveToMarketplaceButton;
        public Button backButton;
        public Button saveButton;

        public InputField newPriceField;

        private ShopItem _shopItem;

        private MarketPlace _marketPlace;

        private IAdventurerScroll _seller;

        private void Start()
        {
            moveToMarketplaceButton.onClick.AddListener(MoveToMarketPlace);
            backButton.onClick.AddListener(CloseUI);
            saveButton.onClick.AddListener(SaveButton);
        }

        public void SetVisible(ShopItem item, MarketPlace marketPlace, IAdventurerScroll seller)
        {
            _shopItem = item;
            gameObject.SetActive(true);

            _marketPlace = marketPlace;
            _seller = seller;
            newPriceField.text = item.price.ToString();
        }

        private void CloseUI()
        {
            _marketPlace.Refresh();
            gameObject.SetActive(false);
        }

        private void MoveToMarketPlace()
        {
            _marketPlace.TransferToShop(_shopItem, _seller);
            CloseUI();
        }

        private void SaveButton()
        {
            _marketPlace.Refresh();
            _shopItem.price = int.Parse(newPriceField.text);
        }
    }
}
