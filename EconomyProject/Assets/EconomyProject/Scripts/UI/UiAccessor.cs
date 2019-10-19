using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class UiAccessor : MonoBehaviour
    {
        public GameAuction GameAuction => coreGameSystem.GetComponentInChildren<GameAuction>();

        public PlayerInput PlayerInput => coreGameSystem.GetComponentInChildren<PlayerInput>();

        [HideInInspector]
        public EconomyAgent economyAgent;

        public GameQuests GameQuests => coreGameSystem.GetComponentInChildren<GameQuests>();

        public GameObject coreGameSystem;


        private void Start()
        {
            economyAgent = coreGameSystem.GetComponentInChildren<EconomyAgent>();
        }
    }
}
