using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public class ShopAgent : Agent
    {
        public int moveAmount = 1;
        public override void CollectObservations()
        {
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            for (var i = 0; i < vectorAction.Length; i++)
            {
                float action = vectorAction[i];
                var moveAction = Mathf.FloorToInt(action);
            }
        }
    }
}
