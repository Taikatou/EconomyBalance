using System;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using Assets.EconomyProject.Scripts.UI.Craftsman.ScrollList;

namespace Assets.EconomyProject.Scripts.UI.Craftsman
{
    public class CraftsmanMakeRequestDropbox : AgentDropDown<CraftsmanAgent>
    {
        public CraftsManMakeRequestScrollList agentScrollList;

        protected override void UpdateChange()
        {
            agentScrollList.UpdateAgent(AgentList[0]);
        }

        protected override void UpdateAgent(CraftsmanAgent agent)
        {
            agentScrollList.UpdateAgent(agent);
        }
    }
}
