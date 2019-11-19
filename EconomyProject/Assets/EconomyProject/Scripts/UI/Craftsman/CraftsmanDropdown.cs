using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.Craftsman
{
    public class CraftsmanDropdown : AgentDropDown<CraftsmanAgent>
    {
        public CraftsmanMenu craftsManMenu;
        protected override void UpdateAgent(CraftsmanAgent agent)
        {
            craftsManMenu.UpdateAgent(agent);
        }
    }
}
