using Assets.EconomyProject.Scripts.MLAgents.Shop;

namespace Assets.EconomyProject.Scripts.UI.Craftsman
{
    public class CraftsmanDropdown : AgentDropDown<ShopAgent>
    {
        public GetCurrentAgent craftsManAccessor;
        protected override void UpdateAgent(ShopAgent agent)
        {
            craftsManAccessor.UpdateAgent(agent);
        }
    }
}
