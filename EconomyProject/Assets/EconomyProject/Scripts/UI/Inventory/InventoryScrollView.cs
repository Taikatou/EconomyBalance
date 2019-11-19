using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;

namespace Assets.EconomyProject.Scripts.UI.Inventory
{
    public class InventoryScrollView : AbstractScrollList<InventoryItem, InventoryScrollButton>
    {
        // Start is called before the first frame update
        public override List<InventoryItem> ItemList => agentInventory.Items;
        public override LastUpdate LastUpdated => agentInventory;

        public AgentInventory agentInventory;

        public override void SelectItem(InventoryItem item, int number = 1)
        {
            throw new System.NotImplementedException();
        }
    }
}
