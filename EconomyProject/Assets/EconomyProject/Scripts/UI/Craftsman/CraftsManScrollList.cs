using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using Assets.EconomyProject.Scripts.UI.ShopUI.Buttons;

namespace Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists
{
    public class CraftsManScrollList : AbstractScrollList<ResourceRequest, CraftingButton>
    {
        public ShopAgent craftingAgent; 
        // Start is called before the first frame update
        public override List<ResourceRequest> ItemList => craftingAgent.GetCraftingRequests();
        public override void SelectItem(ResourceRequest item, int number = 1)
        {
            throw new System.NotImplementedException();
        }
    }
}
