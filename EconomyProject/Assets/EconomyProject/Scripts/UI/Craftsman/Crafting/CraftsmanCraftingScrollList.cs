using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Crafting
{
    public class CraftsmanCraftingScrollList : AbstractScrollList<CraftingInfo, CraftingRequestButton>
    {
        public CraftsmanAgent agent;

        public override List<CraftingInfo> ItemList
        {
            get
            {
                var itemList = new List<CraftingInfo>();
                foreach (var item in agent.CraftingAbility.craftingRequirement)
                {
                    var craftInfo = new CraftingInfo(item, agent.CraftingInventory);
                    itemList.Add(craftInfo);
                }
                return itemList;
            }
        }
        public override LastUpdate LastUpdated => GetComponent<CraftingLastUpdate>();
        public override void SelectItem(CraftingInfo item, int number = 1)
        {
            agent.CraftingAbility.SetCraftingItem(item.craftingMap.choice);
            LastUpdated.Refresh();
        }
    }
}
