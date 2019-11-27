using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems.Requests
{
    public class AdventurerRequestTaker : RequestTaker
    {
        public RequestSystem recordSystem;
        public AdventurerAgent craftsMan;
        public override List<ResourceRequest> ItemList => recordSystem.ResourceRequests;
        public override void TakeRequest(ResourceRequest request)
        {
            recordSystem.TakeRequest(this, request);
        }
    }
}
