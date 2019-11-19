using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.UI.Craftsman.Request.Buttons;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Request.ScrollList
{
    public class CraftsManCurrentRequestScrollList : CraftingScrollList<ResourceRequest, CraftingCurrentRequestButton>
    {
        public CraftsmanMenu getCurrentAgent;

        public RequestTaker RequestTaker => getCurrentAgent.CurrentAgent.GetComponent<RequestTaker>();

        // Start is called before the first frame update
        public override List<ResourceRequest> ItemList => RequestTaker.ItemList;

        public override void SelectItem(ResourceRequest item, int number = 1)
        {
            RequestTaker.TakeRequest(item);
        }
    }
}
