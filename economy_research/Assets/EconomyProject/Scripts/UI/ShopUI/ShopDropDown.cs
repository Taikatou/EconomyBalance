using EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using MLAgents;

namespace EconomyProject.Scripts.UI.ShopUI
{
    public class ShopDropDown : AgentDropDown
    {
        public AgentShopScrollList agentScrollList;

        protected override void UpdateAgent(Agent agent)
        {
            base.UpdateAgent(agent);
            agentScrollList.RefreshDisplay();
        }
    }
}
