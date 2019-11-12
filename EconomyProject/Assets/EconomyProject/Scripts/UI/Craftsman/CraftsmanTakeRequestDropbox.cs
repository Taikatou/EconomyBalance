using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using Assets.EconomyProject.Scripts.UI.Craftsman.ScrollList;

namespace Assets.EconomyProject.Scripts.UI.Craftsman
{
    public class CraftsmanTakeRequestDropbox : AgentDropDown<RequestTaker>
    {
        public CraftsManCurrentRequestScrollList agentScrollList;
        protected override void UpdateChange()
        {
            agentScrollList.UpdateAgent(AgentList[0]);
        }

        protected override void UpdateAgent(RequestTaker agent)
        {
            agentScrollList.UpdateAgent(agent);
        }
    }
}
