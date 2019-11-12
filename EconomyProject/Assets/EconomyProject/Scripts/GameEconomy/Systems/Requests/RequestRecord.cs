using System.Collections.Generic;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests
{
    public class RequestRecord : MonoBehaviour
    {

        private readonly Dictionary<RequestTaker, List<ResourceRequest>> _currentRequests;

        public RequestRecord()
        {
            _currentRequests = new Dictionary<RequestTaker, List<ResourceRequest>>();
        }

        public void AddRequest(RequestTaker requestTaker, ResourceRequest resourceRequest)
        {
            bool contains = _currentRequests.ContainsKey(requestTaker);
            if (!contains)
            {
                _currentRequests[requestTaker] = new List<ResourceRequest>();
            }
            _currentRequests[requestTaker].Add(resourceRequest); 
        }

        private void CompleteRequest(RequestTaker taker, ResourceRequest request)
        {
            if (_currentRequests.ContainsKey(taker))
            {
                var requestList = _currentRequests[taker];
                if (requestList.Contains(request))
                {
                    _currentRequests[taker].Remove(request);
                    request.TransferResource();
                }
            }
        }
    }
}
