using System.Collections.Generic;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public class ShopAgent : Agent
    {
        public float moveAmount = 100;

        public ShopAbility ShopAbility => GetComponent<ShopAbility>();

        public override void CollectObservations()
        {
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            for (var i = 0; i < ShopAbility.shopItems.Count; i++)
            {
                float action = vectorAction[i];
                var moveAction = Mathf.FloorToInt(action);
                var priceChange = moveAction * moveAmount;
                //ShopAbility.UpdatePrice(priceChange);
            }
        }
    }
}
