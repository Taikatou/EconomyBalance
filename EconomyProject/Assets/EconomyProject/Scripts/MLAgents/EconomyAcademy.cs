using Assets.EconomyProject.Scripts.GameEconomy;
using MLAgents;

namespace Assets.EconomyProject.Scripts.MLAgents
{
    public class EconomyAcademy : Academy
    {
        private GameAuction gameAuction => FindObjectOfType<GameAuction>();

        private GameQuests gameQuests => FindObjectOfType<GameQuests>();

        public override void InitializeAcademy()
        {
            Monitor.SetActive(true);
        }

        public override void AcademyReset()
        {
            gameAuction.Reset();
        }
    }
}
