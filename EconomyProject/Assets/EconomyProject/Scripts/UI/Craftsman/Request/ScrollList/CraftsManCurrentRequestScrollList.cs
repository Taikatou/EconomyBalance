using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.UI.Craftsman.Request.Buttons;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Request.ScrollList
{
    public class CraftsManCurrentRequestScrollList : CraftingScrollList<ResourceRequest, CraftingCurrentRequestButton>
    {
        public RequestTaker requestTaker;

        // Start is called before the first frame update
        public override List<ResourceRequest> ItemList => requestTaker.ItemList;

        public override void SelectItem(ResourceRequest item, int number = 1)
        {
            requestTaker.TakeRequest(item);
        }

        public void UpdateAgent(RequestTaker newAgent)
        {
            requestTaker = newAgent;
        }
    }
}
