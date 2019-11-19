using System;
using System.Collections.Generic;
using Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using MLAgents;
using UnityEngine;
using ResourceRequest = Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests.ResourceRequest;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public enum ShopDecision { Ignore, Submit , ChangePriceDown, ChangePriceUp}
    public class ShopAgent : Agent, IAdventurerScroll
    {
        public ShopAbility ShopAbility => GetComponent<ShopAbility>();
        public EconomyWallet Wallet => GetComponent<EconomyWallet>();
        public List<ShopItem> ItemList => ShopAbility.shopItems;
        public MarketPlace MarketPlace => GetComponentInParent<AgentSpawner>().marketPlace;

        public RequestSystem requestSystem;
        public CraftsmanScreen CurrentScreen { get; private set; }
        public CraftingAbility CraftingAbility => GetComponent<CraftingAbility>();
        public CraftingInventory CraftingInventory => GetComponent<CraftingInventory>();
        public AgentInventory AgentInventory => GetComponent<AgentInventory>();

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
            AgentActionShopping(vectorAction, textAction);
        }

        public override void CollectObservations()
        {
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
        public void AgentActionShopping(float[] vectorAction, string textAction)
        {
            
        }

        public void MakeRequest(CraftingResources resource)
        {
            requestSystem.MakeRequest(resource, CraftingInventory);
        }

        public void ChangeAgentScreen(CraftsmanScreen nextScreen)
        {
            CurrentScreen = nextScreen;
        }

        public List<ResourceRequest> GetCraftingRequests()
        {
            return requestSystem.GetCraftingRequests(CraftingInventory);
        }

        public override float[] Heuristic()
        {
            return new float[]{};
        }

        public void SetRequestSystem(RequestSystem newRequestSystem)
        {
            requestSystem = newRequestSystem;
        }
    }
}
