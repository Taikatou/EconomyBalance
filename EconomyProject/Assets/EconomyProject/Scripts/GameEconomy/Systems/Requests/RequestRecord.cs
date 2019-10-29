using System.Collections.Generic;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests
{
    public class RequestRecord
    {
        private static RequestRecord _instance;

        public static RequestRecord Instance => _instance ?? (_instance = new RequestRecord());

        private readonly Dictionary<RequestTaker, List<ResourceRequest>> _currentRequests;

        public RequestRecord()
        {
            _currentRequests = new Dictionary<RequestTaker, List<ResourceRequest>>();
        }

        private void _AddRequest(RequestTaker requestTaker, ResourceRequest resourceRequest)
        {
            bool contains = _currentRequests.ContainsKey(requestTaker);
            if (!contains)
            {
                _currentRequests[requestTaker] = new List<ResourceRequest>();
            }
            _currentRequests[requestTaker].Add(resourceRequest); 
        }

        public static void AddRequest(RequestTaker requestTaker, ResourceRequest resourceRequest)
        {
            Instance._AddRequest(requestTaker, resourceRequest);
        }

        private void _CompleteRequest(RequestTaker taker, ResourceRequest request)
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

        public static void CompleteRequest(RequestTaker taker, ResourceRequest request)
        {
            Instance._CompleteRequest(taker, request);
        }
    }
}
