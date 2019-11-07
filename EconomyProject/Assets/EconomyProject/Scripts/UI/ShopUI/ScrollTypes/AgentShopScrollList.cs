using System.Collections.Generic;
using System.Globalization;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes
{
    public interface IAdventurerScroll
    {
        EconomyWallet Wallet { get; }
        List<Item> ItemList { get; }
        List<Item> ItemInMarket{ get; }
    }
    public class AgentShopScrollList : ShopScrollList
    {
        public override List<Item> ItemList => adventurerAgent?.ItemList;

        public IAdventurerScroll adventurerAgent;

        public Text myGoldDisplay;

        public PooledShopScrollList otherShop;

        public double Gold {
            get
            {
                if (adventurerAgent != null && adventurerAgent.Wallet)
                {
                    return adventurerAgent.Wallet.Money;
                }
                return 0;
            }
            set => adventurerAgent.Wallet?.SetMoney(value);
        }

        public void UpdateAgent(IAdventurerScroll agent)
        {
            adventurerAgent = agent;
            RefreshDisplay();
        }

        public override void RefreshDisplay()
        {
            base.RefreshDisplay();
            myGoldDisplay.text = "Gold: " + Gold.ToString(CultureInfo.InvariantCulture);
        }

        public override void TryTransferItemToOtherShop(Item item)
        {
            otherShop.AddItem(item);
            RemoveItem(item, this);
            adventurerAgent.ItemInMarket.Add(item);
            otherShop.RefreshDisplay();
            RefreshDisplay();
        }
    }
}
