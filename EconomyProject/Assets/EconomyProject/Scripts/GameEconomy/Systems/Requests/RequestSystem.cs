using System;
using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests
{
    public class ResourceRequest
    {
        public int Number { get; }
        public CraftingResources Resources { get; }
        public CraftingInventory Inventory { get; }

        private bool _resourceAdded;

        public ResourceRequest(CraftingResources resources, CraftingInventory inventory, int number = 1)
        {
            Resources = resources;
            Inventory = inventory;
            Number = number;
        }

        public void TransferResource()
        {
            if (!_resourceAdded)
            {
                _resourceAdded = true;
                Inventory.AddResource(Resources, Number);
            }
        }
    }

    public class RequestSystem : MonoBehaviour, ILastUpdate
    {
        public DateTime LastUpdated { get; set; }
        public List<ResourceRequest> ResourceRequests { get; private set; }

        private Dictionary<CraftingInventory, List<ResourceRequest>> _craftingNumber;

        public RequestRecord RequestRecord => GetComponent<RequestRecord>();

        public List<ResourceRequest> GetCraftingRequests(CraftingInventory inventory)
        {
            if (_craftingNumber != null)
            {
                if (_craftingNumber.ContainsKey(inventory))
                {
                    return _craftingNumber[inventory];
                }
            }
            return new List<ResourceRequest>();
        }

        public int GetRequestNumber(CraftingInventory inventory)
        {
            if (_craftingNumber.ContainsKey(inventory))
            {
                return _craftingNumber[inventory].Count;
            }

            return 0;
        }

        private void Start()
        {
            ResourceRequests = new List<ResourceRequest>();
            _craftingNumber = new Dictionary<CraftingInventory, List<ResourceRequest>>();
        }

        public bool MakeRequest(CraftingResources resources, CraftingInventory inventory)
        {
            if (!_craftingNumber.ContainsKey(inventory))
            {
                _craftingNumber.Add(inventory, new List<ResourceRequest>());
            }

            bool canRequest = GetRequestNumber(inventory) < 5;
            if (canRequest)
            {
                var request = new ResourceRequest(resources, inventory);
                ResourceRequests.Add(request);

                _craftingNumber[inventory].Add(request);
            }
            Refresh();
            return canRequest;
        }

        public void RemoveRequest(ResourceRequest takeRequest, CraftingInventory inventory)
        {
            ResourceRequests.Remove(takeRequest);

            _craftingNumber[inventory].Remove(takeRequest);
            Refresh();
        }

        public bool TakeRequest(RequestTaker requestTaker, ResourceRequest takeRequest)
        {
            if (ResourceRequests.Contains(takeRequest))
            {
                RequestRecord.AddRequest(requestTaker, takeRequest);
                ResourceRequests.Remove(takeRequest);
            }
            Refresh();
            return true;
        }

        public void SubmitRequest(CraftingResources resource, CraftingInventory inventory)
        {
            ResourceRequest newRequest = new ResourceRequest(resource, inventory);
            ResourceRequests.Add(newRequest);
            Refresh();
        }

        public void Refresh()
        {
            LastUpdated = DateTime.Now;
        }
    }
}
