using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.ShopUI
{
    public class ShopDropDown : AgentDropDown
    {
        public ShopScrollList shopScroll;

        private void Start()
        {
            dropDown.onValueChanged.AddListener(delegate {
                HandleChange();
            });
        }

        public void HandleChange()
        {
            shopScroll.RefreshDisplay();
        }
    }
}
