using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.UI.Craftsman.ScrollList;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.DropDown
{
    public class CraftsmanMakeRequestDropbox : CraftsmanDropDow<CraftsmanAgent>
    {
        public CraftsManMakeRequestScrollList agentScrollList;

        protected override void UpdateChange()
        {
            agentScrollList.UpdateAgent(AgentList[0]);
        }

        protected override void UpdateAgent(CraftsmanAgent agent)
        {
            base.UpdateAgent(agent);
            agentScrollList.UpdateAgent(agent);
        }
    }
}
