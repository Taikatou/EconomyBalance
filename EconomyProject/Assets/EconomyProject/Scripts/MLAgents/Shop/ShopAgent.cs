using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using Assets.EconomyProject.Scripts.UI.ShopUI;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes;
using MLAgents;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public class ShopAgent : Agent, IAdventurerScroll
    {
        public float moveAmount = 100;

        public ShopAbility ShopAbility => GetComponent<ShopAbility>();

        public override void CollectObservations()
        {
        }

        public override void AgentAction(float[] vectorAction, string textAction)
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
