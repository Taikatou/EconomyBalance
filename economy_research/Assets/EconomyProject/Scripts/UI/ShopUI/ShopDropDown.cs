using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using MLAgents;

namespace Assets.EconomyProject.Scripts.UI.ShopUI
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
