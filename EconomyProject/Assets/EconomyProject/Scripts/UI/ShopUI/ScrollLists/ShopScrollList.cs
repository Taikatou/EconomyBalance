using Assets.EconomyProject.Scripts.MLAgents.Shop;
using Assets.EconomyProject.Scripts.UI.ShopUI.Buttons;

namespace Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists
{
    public abstract class ShopScrollList : AbstractScrollList<ShopItem, ShopButton>
    {
        public MarketPlace marketPlace;

        public override LastUpdate LastUpdated => marketPlace;
    }
}
