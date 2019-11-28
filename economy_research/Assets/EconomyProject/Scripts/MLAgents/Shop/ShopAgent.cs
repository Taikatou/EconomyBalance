using System.Collections.Generic;
using EconomyProject.Scripts.GameEconomy.Systems.Requests;
using EconomyProject.Scripts.Inventory;
using EconomyProject.Scripts.MLAgents.AdventurerAgents;
using EconomyProject.Scripts.MLAgents.Craftsman;
using EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using MLAgents;
using UnityEngine;
using ResourceRequest = EconomyProject.Scripts.GameEconomy.Systems.Requests.ResourceRequest;

namespace EconomyProject.Scripts.MLAgents.Shop
{
    public enum ShopDecision { Ignore, Submit , ChangePriceDown, ChangePriceUp}

    [RequireComponent(typeof(ShopAbility))]
    [RequireComponent(typeof(EconomyWallet))]
    [RequireComponent(typeof(CraftingAbility))]
    [RequireComponent(typeof(CraftingInventory))]
    [RequireComponent(typeof(AgentInventory))]
    public class ShopAgent : Agent
    {
        [HideInInspector]
        public RequestSystem requestSystem;
        public CraftsmanScreen CurrentScreen { get; private set; }
        public List<ShopItem> ItemList => ShopAbility.shopItems;
        public ShopAbility ShopAbility => GetComponent<ShopAbility>();
        public EconomyWallet Wallet => GetComponent<EconomyWallet>();
        public MarketPlace MarketPlace => GetComponentInParent<AgentSpawner>().marketPlace;
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

            AddVectorObs((float)Wallet.EarnedMoney);
            AddVectorObs((float)Wallet.SpentMoney);
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
