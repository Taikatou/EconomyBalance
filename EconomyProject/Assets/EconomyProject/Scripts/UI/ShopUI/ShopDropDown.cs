using Assets.EconomyProject.Scripts.MLAgents.Shop;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;

namespace Assets.EconomyProject.Scripts.UI.ShopUI
{
    public class ShopDropDown : AgentDropDown<ShopAgent>
    {
        public AgentShopScrollList agentScrollList;

        protected override void UpdateAgent(ShopAgent agent)
        {
            agentScrollList.UpdateAgent(agent);
        }

        protected override void UpdateChange()
        {
            agentScrollList.UpdateAgent(AgentList[0]);
        }
    }
}
