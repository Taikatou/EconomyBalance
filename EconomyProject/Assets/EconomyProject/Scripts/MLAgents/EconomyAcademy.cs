using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using MLAgents;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class EconomyAcademy : Academy
    {
        private static GameAuction GameAuction => FindObjectOfType<GameAuction>();

        private static PlayerInput PlayerInput => FindObjectOfType<PlayerInput>();

        public override void InitializeAcademy()
        {
            Monitor.SetActive(true);
        }

        public override void AcademyReset()
        {
            GameAuction.Reset();
            PlayerInput.Reset();
        }
    }
}
