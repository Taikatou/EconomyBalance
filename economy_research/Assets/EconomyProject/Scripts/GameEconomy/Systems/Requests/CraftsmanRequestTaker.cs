using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Shop;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests
{
    public class CraftsmanRequestTaker : RequestTaker
    {
        public ShopAgent CraftsMan => GetComponent<ShopAgent>();
        public override List<ResourceRequest> ItemList => CraftsMan.GetCraftingRequests();
        public override void TakeRequest(ResourceRequest request)
        {
            CraftsMan.requestSystem.RemoveRequest(request, CraftsMan.CraftingInventory);
        }
    }
}
