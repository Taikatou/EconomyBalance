using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman.Requirements;
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

    public class RequestSystem : MonoBehaviour
    {
        private List<ResourceRequest> _resourceRequests;

        private Dictionary<CraftingInventory, List<ResourceRequest>> _craftingNumber;

        public int GetRequestNumber(CraftingInventory inventory)
        {
            return _craftingNumber[inventory].Count;
        }

        private void Start()
        {
            _resourceRequests = new List<ResourceRequest>();
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
                _resourceRequests.Add(request);

                _craftingNumber[inventory].Add(request);
            }

            return canRequest;
        }

        public bool TakeRequest(int index, RequestTaker requestTaker)
        {
            if (_resourceRequests.Count > index)
            {
                var request = _resourceRequests[index];
                RequestRecord.AddRequest(requestTaker, request);
                _resourceRequests.RemoveAt(index);
            }
            return true;
        }

        public void SubmitRequest(CraftingResources resource, CraftingInventory inventory)
        {
            ResourceRequest newRequest = new ResourceRequest(resource, inventory);
            _resourceRequests.Add(newRequest);
        }

    }
}
