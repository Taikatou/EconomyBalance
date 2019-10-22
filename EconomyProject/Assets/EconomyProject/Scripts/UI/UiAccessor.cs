using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class UiAccessor : MonoBehaviour
    {
        private EconomyAgent _economyAgent;
        
        public GameObject coreGameSystem;

        public GameQuests GameQuests => coreGameSystem.GetComponentInChildren<GameQuests>();

        public PlayerInput PlayerInput => coreGameSystem.GetComponentInChildren<PlayerInput>();

        public GameAuction GameAuction => coreGameSystem.GetComponentInChildren<GameAuction>();
        public EconomyAgent EconomyAgent
        {
            get
            {
                if (_economyAgent == null)
                {
                    _economyAgent = coreGameSystem.GetComponentInChildren<EconomyAgent>();
                }
                return _economyAgent;
            }
        }
    }
}
