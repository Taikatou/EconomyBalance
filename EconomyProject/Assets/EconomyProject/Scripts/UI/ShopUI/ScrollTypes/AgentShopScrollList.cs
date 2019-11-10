using System.Collections.Generic;
using System.Globalization;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using Assets.EconomyProject.Scripts.MLAgents.Shop;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes
{
    public interface IAdventurerScroll
    {
        EconomyWallet Wallet { get; }
        List<ShopItem> ItemList { get; }
    }
    public class AgentShopScrollList : ShopScrollList
    {
        public override List<ShopItem> ItemList => adventurerAgent?.ItemList;

        public IAdventurerScroll adventurerAgent;

        public Text myGoldDisplay;

        public PooledShopScrollList otherShop;

        public double Gold
        {
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

        protected override void RemoveItem(ShopItem itemToRemove)
        {
            ItemList.Remove(itemToRemove);
        }

        public override void TryTransferItemToOtherShop(ShopItem item)
        {
            otherShop.AddItem(item, adventurerAgent);
            RemoveItem(item);
            otherShop.RefreshDisplay();
            RefreshDisplay();
        }
    }
}
