﻿using System;
using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents;
using MLAgents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI
{
    public class AgentDropDown : MonoBehaviour
    {
        public Dropdown dropDown;

        private readonly HashSet<string> _agentIds  = new HashSet<string>();

        public Agent[] AgentList => getCurrentAgent.GetAgents;

        private bool _setOption;

        public GetCurrentAgent getCurrentAgent;

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
                UpdateChange();
            }
        }

        protected virtual void UpdateChange()
        {

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
                if (agent.GetComponent<AgentID>().agentId == Int32.Parse(id))
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
