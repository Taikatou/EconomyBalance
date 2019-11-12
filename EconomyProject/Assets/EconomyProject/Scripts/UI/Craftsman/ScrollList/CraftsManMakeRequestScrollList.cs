using System;
using System.Collections.Generic;
using System.Linq;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using Assets.EconomyProject.Scripts.UI.Craftsman.Buttons;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.ScrollList
{
    public class CraftsManMakeRequestScrollList : CraftingScrollList<CraftingResources, CraftingMakeRequestButton>
    {
        public CraftsmanAgent craftingAgent;
        // Start is called before the first frame update
        public override List<CraftingResources> ItemList => Enum.GetValues(typeof(CraftingResources)).Cast<CraftingResources>().ToList();

        public override void SelectItem(CraftingResources item, int number = 1)
        {
            craftingAgent.MakeRequest(item);
        }

        public void UpdateAgent(CraftsmanAgent newAgent)
        {
            craftingAgent = newAgent;
        }
    }
}
