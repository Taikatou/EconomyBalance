using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class UiAccessor : MonoBehaviour
    {
        private AdventurerAgent _adventurerAgent;
        
        public GameObject coreGameSystem;

        public GameQuests GameQuests => coreGameSystem.GetComponentInChildren<GameQuests>();

        public PlayerInput PlayerInput => coreGameSystem.GetComponentInChildren<PlayerInput>();

        public GameAuction GameAuction => coreGameSystem.GetComponentInChildren<GameAuction>();
        public AdventurerAgent AdventurerAgent
        {
            get
            {
                if (_adventurerAgent == null)
                {
                    _adventurerAgent = coreGameSystem.GetComponentInChildren<AdventurerAgent>();
                    //_adventurerAgent.printObservations = true;
                }
                return _adventurerAgent;
            }
        }
    }
}
