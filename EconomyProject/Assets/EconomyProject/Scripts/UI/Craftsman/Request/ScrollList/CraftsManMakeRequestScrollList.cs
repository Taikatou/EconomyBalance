using System;
using System.Collections.Generic;
using System.Linq;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using Assets.EconomyProject.Scripts.UI.Craftsman.Request.Buttons;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Request.ScrollList
{
    public class CraftingResourceUi
    {
        public CraftingResources resourceType;
        public int inventoryNumber;

        public CraftingResourceUi (CraftingResources resourceType, int inventoryNumber)
        {
            this.resourceType = resourceType;
            this.inventoryNumber = inventoryNumber;
        }
    }
    public class CraftsManMakeRequestScrollList : CraftingScrollList<CraftingResourceUi, CraftingMakeRequestButton>
    {
        public CraftsmanAgent craftingAgent;

        // Start is called before the first frame update
        public override List<CraftingResourceUi> ItemList
        {
            get
            {
                var items = new List<CraftingResourceUi>();
                var resources = Enum.GetValues(typeof(CraftingResources)).Cast<CraftingResources>().ToList();
                foreach(var resource in resources)
                {
                    var inventoryNumber = craftingAgent.CraftingInventory.GetResourceNumber(resource);
                    var resourceUi = new CraftingResourceUi(resource, inventoryNumber);
                    items.Add(resourceUi);
                }
                return items;
            }
        } 

        public override void SelectItem(CraftingResourceUi item, int number = 1)
        {
            craftingAgent.MakeRequest(item.resourceType);
        }

        public void UpdateAgent(CraftsmanAgent newAgent)
        {
            craftingAgent = newAgent;
        }
    }
}
