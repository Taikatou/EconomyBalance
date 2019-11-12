using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.UI.ShopUI.Buttons;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.ScrollList
{
    public abstract class CraftingScrollList<T, TQ> : AbstractScrollList<T, TQ> where TQ : SampleButton<T>
    {
        public override ILastUpdate LastUpdated => requestSystem;

        public RequestSystem requestSystem;
    }
}
