using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
namespace Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests
{
    public class AdventurerRequestTaker : RequestTaker
    {
        public RequestSystem recordSystem;
        public AdventurerAgent CraftsMan => GetComponent<AdventurerAgent>();
        public override List<ResourceRequest> ItemList => recordSystem.ResourceRequests;
        public override void TakeRequest(ResourceRequest request)
        {
            recordSystem.TakeRequest(this, request);
        }
    }
}
