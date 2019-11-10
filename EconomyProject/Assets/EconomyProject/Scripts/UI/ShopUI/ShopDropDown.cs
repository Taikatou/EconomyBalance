using System;
using Assets.EconomyProject.Scripts.MLAgents;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes;

namespace Assets.EconomyProject.Scripts.UI.ShopUI
{
    public class ShopDropDown : AgentDropDown
    {
        public AgentShopScrollList agentScrollList;

        private void Start()
        {
            dropDown.onValueChanged.AddListener(delegate {
                HandleChange();
            });
        }

        public void HandleChange()
        {
            var id = dropDown.options[dropDown.value].text;
            foreach (var agent in AgentList)
            {
                if (agent.GetComponent<AgentID>().agentId == Int32.Parse(id))
                {
                    agentScrollList.UpdateAgent(agent);
                }
            }
        }

        protected override void UpdateChange()
        {
            agentScrollList.UpdateAgent(AgentList[0]);
        }
    }
}
