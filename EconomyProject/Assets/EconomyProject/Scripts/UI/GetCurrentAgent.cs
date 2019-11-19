using System;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public abstract class GetCurrentAgent<T> : MonoBehaviour where T: Agent
    {
        public abstract GameObject AgentParent { get; }

        public int Index { get; set; }

        public T[] GetAgents => AgentParent.GetComponentsInChildren<T>();

        public T CurrentAgent
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

        public void UpdateAgent(T agent)
        {
            var index = Array.FindIndex(GetAgents, a => a == agent);
            if (index >= 0)
            {
                Index = index;
            }
        }
    }
}
