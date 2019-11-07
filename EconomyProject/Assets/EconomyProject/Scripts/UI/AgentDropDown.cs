using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI
{
    public class AgentDropDown : MonoBehaviour
    {
        public Dropdown dropDown;

        public GameObject agentList;

        private HashSet<string> _agentIds;

        public ShopAgent[] AgentList => agentList.GetComponentsInChildren<ShopAgent>();

        private void Start()
        {
            _agentIds = new HashSet<string>();
        }

        // Update is called once per frame
        private void Update()
        {
            foreach (var agent in AgentList)
            {
                AgentID agentId = agent.GetComponent<AgentID>();
                if (agentId != null)
                {
                    string agentIdStr = agentId.agentId.ToString();
                    if (!_agentIds.Contains(agentIdStr))
                    {
                        _agentIds.Add(agentIdStr);
                        dropDown.options.Add(new Dropdown.OptionData(agentIdStr));
                    }
                }
            }
        }
    }
}
