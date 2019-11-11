using System;
using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public enum ShopDecision { Ignore, Submit , ChangePriceDown, ChangePriceUp}
    public class ShopAgent : CraftsmanAgent, IAdventurerScroll
    {
        public int moveAmount = 1;
        public ShopAbility ShopAbility => GetComponent<ShopAbility>();
        public EconomyWallet Wallet => GetComponent<EconomyWallet>();
        public List<ShopItem> ItemList => ShopAbility.shopItems;
        public MarketPlace MarketPlace => GetComponentInParent<ShopSpawner>().marketPlace;

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
            CollectObservationsShop();
        }

        public void CollectObservationsShop()
        {
            AddVectorObs(MarketPlace.SellersItems(this));

            AddVectorObs((float)Wallet.earnedMoney);
            AddVectorObs((float)Wallet.spentMoney);
            AddVectorObs((float)Wallet.Money);
            Wallet.ResetStep();
        }

        // continuous decisions 6 change price
            // discrete submit to marketplace
        public void AgentActionShopping(float[] vectorAction, string textAction)
        {
            var selectedItem = Mathf.FloorToInt(vectorAction[0]);

            var item = ShopAbility.shopItems[selectedItem];

            var shopDecision = Mathf.FloorToInt(vectorAction[1]);
            var decision = (ShopDecision)shopDecision;
            switch (decision)
            {
                case ShopDecision.Ignore:
                    break;
                case ShopDecision.Submit:
                    MarketPlace.TransferToShop(item, this);
                    break;
                case ShopDecision.ChangePriceDown:
                    item.DecreasePrice(moveAmount);
                    break;
                case ShopDecision.ChangePriceUp:
                    item.IncreasePrice(moveAmount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
