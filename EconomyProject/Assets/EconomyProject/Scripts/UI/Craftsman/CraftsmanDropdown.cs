using Assets.EconomyProject.Scripts.MLAgents.Craftsman;

namespace Assets.EconomyProject.Scripts.UI.Craftsman
{
    public class CraftsmanDropdown : AgentDropDown<CraftsmanAgent>
    {
        public CraftsmanGetAgent craftsManMenu;
        protected override void UpdateAgent(CraftsmanAgent agent)
        {
            craftsManMenu.UpdateAgent(agent);
        }
    }
}
