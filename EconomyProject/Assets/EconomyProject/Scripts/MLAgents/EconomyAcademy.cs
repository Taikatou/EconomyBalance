using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.Inventory;
using MLAgents;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class EconomyAcademy : Academy
    {

        public override void InitializeAcademy()
        {
            Monitor.SetActive(true);
        }

    }
}
