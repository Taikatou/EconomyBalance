using System.Collections.Generic;
using EconomyProject.Scripts.MLAgents.Craftsman;
using EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
using EconomyProject.Scripts.UI.ShopUI.ScrollLists;

namespace EconomyProject.Scripts.GameEconomy.Systems.Requests
{
    public class ResourceRequest
    {
        private bool _resourceAdded;
        public int Number { get; }
        public CraftingResources Resources { get; }
        public CraftingInventory Inventory { get; }

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

    public class RequestSystem : LastUpdate
    {
        public RequestRecord requestRecord;

        public List<ResourceRequest> ResourceRequests { get; private set; }

        private Dictionary<CraftingInventory, List<ResourceRequest>> _craftingNumber;

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

        public List<ResourceRequest> GetAllCraftingRequests()
        {
            var returnList = new List<ResourceRequest>();
            foreach (KeyValuePair<CraftingInventory, List<ResourceRequest>> entry in _craftingNumber)
            {
                foreach (var item in entry.Value)
                {
                    returnList.Add(item);
                }
            }

            return returnList;
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
            Refresh();
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
                requestRecord.AddRequest(requestTaker, takeRequest);
                ResourceRequests.Remove(takeRequest);
            }

            Refresh();
            return true;
        }

        public void CompleteRequest(RequestTaker taker, ResourceRequest request)
        {
            requestRecord.CompleteRequest(taker, request);
            if (_craftingNumber.ContainsKey(request.Inventory))
            {
                if (_craftingNumber[request.Inventory].Contains(request))
                {
                    _craftingNumber[request.Inventory].Remove(request);
                    var count = _craftingNumber[request.Inventory].Count;
                    if (count == 0)
                    {
                        _craftingNumber.Remove(request.Inventory);
                    }
                }
            }
        }
    }
}
