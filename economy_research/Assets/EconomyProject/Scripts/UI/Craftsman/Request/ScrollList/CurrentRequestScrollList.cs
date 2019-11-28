using System.Collections.Generic;
using EconomyProject.Scripts.GameEconomy.Systems.Requests;
using EconomyProject.Scripts.UI.Craftsman.Request.Buttons;

namespace EconomyProject.Scripts.UI.Craftsman.Request.ScrollList
{
    public class CurrentRequestScrollList : CraftingScrollList<ResourceRequest, CraftingCurrentRequestButton>
    {
        public GetCurrentAgent getCurrentAgent;

        public RequestTaker RequestTaker => getCurrentAgent?.CurrentAgent?.GetComponent<RequestTaker>();

        // Start is called before the first frame update
        public override List<ResourceRequest> ItemList => RequestTaker ? RequestTaker.ItemList : null;

        public override void SelectItem(ResourceRequest item, int number = 1)
        {
            RequestTaker?.TakeRequest(item);
        }
    }
}
