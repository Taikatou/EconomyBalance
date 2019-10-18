﻿using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.MLAgents;
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

        public EconomyAgent economyAgent;

        // Update is called once per frame
        private void Update()
        {
            GameAuction gameAuction = FindObjectOfType<GameAuction>();
            if(gameAuction)
            {
                auctionText.text = "Auction Items: " + gameAuction.ItemCount.ToString();
            }
            if(economyAgent)
            {
                moneyText.text = "MONEY: " + economyAgent.Money;

                if(economyAgent.Item)
                {
                    durabilityText.text = "DURABILITY: " + (economyAgent.Item.unBreakable? "∞" : economyAgent.Item.durability.ToString());

                    currentItemText.text = "CURRENT ITEM: " + economyAgent.Item.Name;

                    efficiencyText.text = "EFFICIENCY: " + economyAgent.Item.efficiency;
                }
            }
        }
    }
}
