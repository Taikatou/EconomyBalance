using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.Adventurer
{
    public class AdventurerGetAgent : GetCurrentAgent<AdventurerAgent>
    {
        public GameObject coreGameSystem;
        public override GameObject AgentParent => coreGameSystem;
    }
}
