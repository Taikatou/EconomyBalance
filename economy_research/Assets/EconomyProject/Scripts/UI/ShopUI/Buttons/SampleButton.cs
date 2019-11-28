using EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using UnityEngine;
using UnityEngine.UI;

namespace EconomyProject.Scripts.UI.ShopUI.Buttons
{
    public abstract class SampleButton <T> : MonoBehaviour
    {
        public Button buttonComponent;

        protected IScrollList<T> scrollList;

        protected T itemDetails;

        // Use this for initialization
        private void Start()
        {
            buttonComponent.onClick.AddListener(HandleClick);
        }

        public void Setup(T currentItemDetails, IScrollList<T> currentScrollList)
        {
            itemDetails = currentItemDetails;
            scrollList = currentScrollList;
            SetupButton();
        }

        public abstract void SetupButton();

        public void HandleClick()
        {
            var number = 1;
            scrollList.SelectItem(itemDetails, number);
        }
    }
}