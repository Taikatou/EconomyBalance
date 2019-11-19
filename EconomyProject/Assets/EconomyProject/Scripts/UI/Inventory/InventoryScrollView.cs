using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.UI.Craftsman;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;

namespace Assets.EconomyProject.Scripts.UI.Inventory
{
    public class InventoryScrollView : AbstractScrollList<InventoryItem, InventoryScrollButton>
    {
        // Start is called before the first frame update
        public override List<InventoryItem> ItemList => AgentInventory.Items;
        public override LastUpdate LastUpdated => AgentInventory;

        public CraftsmanGetAgent craftsmanMenu;

        public AgentInventory AgentInventory => craftsmanMenu.CurrentAgent.AgentInventory;

        public override void SelectItem(InventoryItem item, int number = 1)
        {
            throw new System.NotImplementedException();
        }
    }
}
