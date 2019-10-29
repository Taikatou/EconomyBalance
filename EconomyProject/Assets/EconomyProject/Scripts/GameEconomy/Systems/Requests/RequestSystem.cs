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

        private void Start()
        {
            _resourceRequests = new List<ResourceRequest>();
        }

        public void MakeRequest(CraftingResources resources, CraftingInventory inventory)
        {
            var request = new ResourceRequest(resources, inventory);
            _resourceRequests.Add(request);
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
