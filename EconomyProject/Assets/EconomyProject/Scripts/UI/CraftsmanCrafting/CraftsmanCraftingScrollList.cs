using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;

namespace Assets.EconomyProject.Scripts.UI.CraftsmanCrafting
{
    public class CraftsmanCraftingScrollList : AbstractScrollList<CraftingMap, CraftingRequestButton>
    {
        public CraftsmanAgent agent;
        public override List<CraftingMap> ItemList => agent.CraftingAbility.craftingRequirement;
        public override LastUpdate LastUpdated { get; }
        public override void SelectItem(CraftingMap item, int number = 1)
        {
            throw new System.NotImplementedException();
        }
    }
}
