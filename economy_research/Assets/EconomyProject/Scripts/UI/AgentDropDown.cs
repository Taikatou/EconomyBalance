using System;
using System.Collections.Generic;
using EconomyProject.Scripts.MLAgents;
using MLAgents;
using UnityEngine;
using UnityEngine.UI;

namespace EconomyProject.Scripts.UI
{
    public class AgentDropDown : MonoBehaviour
    {
        public Dropdown dropDown;

        private readonly HashSet<string> _agentIds = new HashSet<string>();

        private bool _setOption;

        public GetCurrentAgent getCurrentAgent;

        public Agent[] AgentList => getCurrentAgent.GetAgents;

        // Update is called once per frame
        private void Update()
        {
            foreach (var agent in AgentList)
            {
                var agentId = agent.GetComponent<AgentID>();
                if (agentId != null)
                {
                    var agentIdStr = agentId.agentId.ToString();
                    if (!_agentIds.Contains(agentIdStr))
                    {
                        _agentIds.Add(agentIdStr);
                        dropDown.options.Add(new Dropdown.OptionData(agentIdStr));
                    }
                }
            }

            if (!_setOption && AgentList.Length > 0)
            {
                _setOption = true;
                dropDown.value = 0;
            }
        }

        private void Start()
        {
            dropDown.onValueChanged.AddListener(delegate {
                HandleChange();
            });
        }

        protected virtual void HandleChange()
        {
            var id = dropDown.options[dropDown.value].text;
            foreach (var agent in AgentList)
            {
                var agentId = agent.GetComponent<AgentID>();
                if (agentId.agentId == int.Parse(id))
                {
                    UpdateAgent(agent);
                }
            }
        }

        protected virtual void UpdateAgent(Agent agent)
        {
            getCurrentAgent.UpdateAgent(agent);
        }
    }
}
