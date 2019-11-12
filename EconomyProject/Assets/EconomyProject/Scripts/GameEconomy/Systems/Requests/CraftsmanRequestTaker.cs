using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests
{
    public class CraftsmanRequestTaker : RequestTaker
    {
        public CraftsmanAgent CraftsMan => GetComponent<CraftsmanAgent>();
        public override List<ResourceRequest> ItemList => CraftsMan.GetCraftingRequests();
        public override void TakeRequest(ResourceRequest request)
        {
            CraftsMan.requestSystem.RemoveRequest(request, CraftsMan.CraftingInventory);
        }
    }
}
