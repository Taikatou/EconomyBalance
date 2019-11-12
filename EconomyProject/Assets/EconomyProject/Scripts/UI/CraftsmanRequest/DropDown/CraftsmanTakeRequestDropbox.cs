using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.UI.CraftsmanRequest.ScrollList;

namespace Assets.EconomyProject.Scripts.UI.CraftsmanRequest.DropDown
{
    public class CraftsmanTakeRequestDropbox : CraftsmanDropDow<RequestTaker>
    {
        public CraftsManCurrentRequestScrollList agentScrollList;
        protected override void UpdateChange()
        {
            agentScrollList.UpdateAgent(AgentList[0]);
        }

        protected override void UpdateAgent(RequestTaker agent)
        {
            base.UpdateAgent(agent);
            agentScrollList.UpdateAgent(agent);
        }
    }
}
