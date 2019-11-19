using System;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class GetCurrentAgent : MonoBehaviour
    {
        public GameObject agentParent;

        public int Index { get; set; }

        public Agent[] GetAgents => agentParent.GetComponentsInChildren<Agent>();

        public Agent CurrentAgent
        {
            get
            {
                if (GetAgents.Length > Index)
                {
                    return GetAgents[Index];
                }
                return null;
            }
        }

        public void UpdateAgent(Agent agent)
        {
            var index = Array.FindIndex(GetAgents, a => a == agent);
            if (index >= 0)
            {
                Index = index;
            }
        }
    }
}
