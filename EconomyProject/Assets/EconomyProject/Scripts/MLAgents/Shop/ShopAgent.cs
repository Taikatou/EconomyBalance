using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollTypes;

namespace Assets.EconomyProject.Scripts.MLAgents.Shop
{
    public class ShopAgent : CraftsmanAgent, IAdventurerScroll
    {
        public float moveAmount = 100;

        public ShopAbility ShopAbility => GetComponent<ShopAbility>();

        public EconomyWallet Wallet => GetComponent<EconomyWallet>();
        public List<ShopItem> ItemList => ShopAbility.shopItems;

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            AgentActionCrafting(vectorAction, textAction);
            AgentActionShopping(vectorAction, textAction);
        }

        public override void CollectObservations()
        {
            CollectObservationsCrafting();
        }

        public void AgentActionShopping(float[] vectorAction, string textAction)
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
