using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using MLAgents;

namespace Assets.EconomyProject.Scripts.UI.ShopUI.Agents
{
    public class AgentShopScrollList : ShopScrollList
    {
        // Start is called before the first frame update
        public override List<Item> ItemList { get; set; }

        public Agent adventurerAgent;

        private EconomyWallet Wallet => adventurerAgent.GetComponent<EconomyWallet>();

        public override double Gold {
            get => Wallet.Money;
            set => Wallet.SetMoney(value);
        }
    }
}
