using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using Assets.EconomyProject.Scripts.UI.Adventurer;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI
{
    public class GeneralUi : MonoBehaviour
    {
        public Text auctionText;

        public Text moneyText;

        public Text durabilityText;

        public Text currentItemText;

        public Text efficiencyText;

        public UiAccessor accessor;

        public GetCurrentAgent getAgent;

        public Dropdown dropDown;

        private HashSet<string> _agentIds;

        public AdventurerAgent AdventurerAgent
        {
            get
            {
                var adventurer = getAgent.CurrentAgent.GetComponent<AdventurerAgent>();
                return adventurer;
            }
        }

        private void Start()
        {
            _agentIds = new HashSet<string>();
        }

        // Update is called once per frame
        private void Update()
        {
            var gameAuction = accessor.GameAuction;
            if (gameAuction)
            {
                auctionText.text = "Auction Items: " + gameAuction.ItemCount;
            }
            if(AdventurerAgent)
            {
                moneyText.text = "MONEY: " + AdventurerAgent.Wallet.Money;

                if(AdventurerAgent.Item)
                {
                    durabilityText.text = "DURABILITY: " + (AdventurerAgent.Item.unBreakable? "∞" : AdventurerAgent.Item.durability.ToString());

                    currentItemText.text = "CURRENT ITEM: " + AdventurerAgent.Item.itemName;

                    efficiencyText.text = "EFFICIENCY: " + AdventurerAgent.Item.efficiency;
                }
            }

            // todo add this to dropdown text
            /*foreach (var agent in AdventurerAgent.GetAgents)
            {
                string agentId = agent.GetComponent<AgentID>().agentId.ToString();
                if (!_agentIds.Contains(agentId))
                {
                    _agentIds.Add(agentId);
                    dropDown.options.Add(new Dropdown.OptionData(agentId));
                }
            }
            AdventurerAgent.Index = dropDown.value;*/
        }
    }
}
