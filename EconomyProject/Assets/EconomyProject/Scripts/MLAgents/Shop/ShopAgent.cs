using MLAgents;

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
                var priceChange = action * moveAmount;

                var item = ShopAbility.shopItems[i];
                ShopAbility.ChangePrice(item, (int)priceChange);
            }
        }
    }
}
