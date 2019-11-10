using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public enum ShopDecision { Ignore, Submit }
    public class ShopAgent : CraftsmanAgent, IAdventurerScroll
    {
        public int moveAmount = 1;
        public ShopAbility ShopAbility => GetComponent<ShopAbility>();
        public EconomyWallet Wallet => GetComponent<EconomyWallet>();
        public List<ShopItem> ItemList => ShopAbility.shopItems;

        public void RemoveItem(ShopItem itemToRemove)
        {
            var sellersItem = FindItem(itemToRemove);
            Debug.Log(sellersItem.stock);
            var toRemove = sellersItem.DeductStock(itemToRemove.stock);
            if (toRemove)
            {
                ItemList.Remove(sellersItem);
            }
        }

        public void AddItem(ShopItem addItem)
        {
            var foundItem = FindItem(addItem);
            if (foundItem != null)
            {
                foundItem.IncreaseStock(addItem.stock);
            }
            else
            {
                ItemList.Add(addItem);
            }
        }

        public bool HasItem(ShopItem toCompare)
        {
            return FindItem(toCompare) != null;
        }

        public ShopItem FindItem(ShopItem toCompare)
        {
            foreach (var item in ItemList)
            {
                if (ShopItem.Compare(item, toCompare))
                {
                    return item;
                }
            }

            return null;
        }


        public override void AgentAction(float[] vectorAction, string textAction)
        {
            AgentActionCrafting(vectorAction, textAction);
            AgentActionShopping(vectorAction, textAction);
        }

        public override void CollectObservations()
        {
            CollectObservationsCrafting();
        }

        public int ChangeAmount(int action)
        {
            switch (action)
            {
                case 1:
                    return moveAmount;
                case 2:
                    return -moveAmount;
            }

            return 0;
        }

        // continuous decisions 6 change price
        // discrete submit to marketplace
        public void AgentActionShopping(float[] vectorAction, string textAction)
        {
            for (var i = 0; i < ShopAbility.shopItems.Count; i++)
            {
                var action = Mathf.FloorToInt(vectorAction[i]);
                var priceChange = ChangeAmount(action);

                var item = ShopAbility.shopItems[i];
                ShopAbility.ChangePrice(item, priceChange);
            }

            for (var j = 5; j < 12; j++)
            {
                var action = Mathf.FloorToInt(vectorAction[j]);
                var decision = (ShopDecision) action;
                switch (decision)
                {
                    case ShopDecision.Ignore:
                        break;
                    case ShopDecision.Submit:
                        break;
                }
            }
        }
    }
}
