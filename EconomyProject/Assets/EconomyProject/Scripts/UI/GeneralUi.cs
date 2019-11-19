using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents;
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

        public AdventurerGetAgent adventurerGetAgent;

        public Dropdown dropDown;

        private HashSet<string> _agentIds;

        private void Start()
        {
            _agentIds = new HashSet<string>();
        }

        // Update is called once per frame
        private void Update()
        {
            var adventurerAgent = adventurerGetAgent.CurrentAgent;
            var gameAuction = accessor.GameAuction;
            if (gameAuction)
            {
                auctionText.text = "Auction Items: " + gameAuction.ItemCount.ToString();
            }
            if(adventurerAgent)
            {
                moneyText.text = "MONEY: " + adventurerAgent.Wallet.Money;

                if(adventurerAgent.Item)
                {
                    durabilityText.text = "DURABILITY: " + (adventurerAgent.Item.unBreakable? "∞" : adventurerAgent.Item.durability.ToString());

                    currentItemText.text = "CURRENT ITEM: " + adventurerAgent.Item.itemName;

                    efficiencyText.text = "EFFICIENCY: " + adventurerAgent.Item.efficiency;
                }
            }

            foreach (var agent in adventurerGetAgent.GetAgents)
            {
                string agentId = agent.GetComponent<AgentID>().agentId.ToString();
                if (!_agentIds.Contains(agentId))
                {
                    _agentIds.Add(agentId);
                    dropDown.options.Add(new Dropdown.OptionData(agentId));
                }
            }
            adventurerGetAgent.Index = dropDown.value;
        }
    }
}
