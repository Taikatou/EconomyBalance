using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using Assets.EconomyProject.Scripts.UI.ShopUI;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public class ShopAgent : CraftsmanAgent, IAdventurerScroll
    {
        public float moveAmount = 100;

        public ShopAbility ShopAbility => GetComponent<ShopAbility>();

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            AgentActionCrafting(vectorAction, textAction);
            AgentActionShopping(vectorAction, textAction);
        }

        public override void CollectObservations()
        {
            CollectObservationsCrafting();
        }

        public void AgentActionShopping(float[] vectorAction, string textAction)
        {
            for (var i = 0; i < ShopAbility.shopItems.Count; i++)
            {
                float action = vectorAction[i];
                var priceChange = action * moveAmount;

                var item = ShopAbility.shopItems[i];
                ShopAbility.ChangePrice(item, (int)priceChange);
            }
        }

        public EconomyWallet Wallet => GetComponent<EconomyWallet>();

        public List<Item> ItemList
        {
            get
            {
                var itemList = new List<Item>();
                foreach (var item in ShopAbility.shopItems)
                {
                    var shopItem = ShopAbility.GetItemPrice(item);
                    if (shopItem.HasValue)
                    {
                        ShopItem value = shopItem.Value;
                        for (var i = 0; i < value.number; i++)
                        {
                            var uiItem = new Item(item.itemName, (double)value.price);
                            itemList.Add(uiItem);
                        }
                    }
                }

                return itemList;
            }
        }

        public override void InitializeAgent()
        {
            ItemInMarket = new List<Item>();
        }

        public List<Item> ItemInMarket { get; set; }
    }
}
