using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Shop;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests
{
    public class CraftsmanRequestTaker : RequestTaker
    {
        public ShopAgent craftsMan;
        public override List<ResourceRequest> ItemList => craftsMan.GetCraftingRequests();
        public override void TakeRequest(ResourceRequest request)
        {
            craftsMan.requestSystem.RemoveRequest(request, craftsMan.CraftingInventory);
        }
    }
}
