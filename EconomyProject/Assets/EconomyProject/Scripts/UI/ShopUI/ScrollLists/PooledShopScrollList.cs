using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Shop;

namespace Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists
{
    public class PooledShopScrollList : ShopScrollList
    {
        // Start is called before the first frame update
        public override List<ShopItem> ItemList => marketPlace.ItemList;

        public override void SelectItem(ShopItem item, int number = 1)
        {
            marketPlace.TryTransferItemToOtherShop(item, otherShop);
        }

        public AgentShopScrollList otherShop;
    }
}
