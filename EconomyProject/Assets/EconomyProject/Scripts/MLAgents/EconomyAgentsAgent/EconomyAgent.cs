using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.Inventory;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent
{
    public enum AgentScreen { Main, Quest, Auction }

    public enum AuctionChoice { Ignore, Bid }

    public class EconomyAgent : Agent
    {

        public static int agentCounter = 0;

        [HideInInspector] public int agentId;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

        public InventoryItem Item => Inventory.EquipedItem;

        private GameAuction gameAuction => FindObjectOfType<GameAuction>();

        public PlayerInput PlayerInput => FindObjectOfType<PlayerInput>();

        public AgentScreen ChosenScreen => PlayerInput.GetScreen(this);

        public EconomyWallet Wallet => GetComponent<EconomyWallet>();

        public override void AgentReset()
        {
            Inventory.ResetInventory();
        }

        public override void InitializeAgent()
        {
            agentId = agentCounter;
            agentCounter++;
        }

        public void BoughtItem(InventoryItem item, float cost)
        {
            Inventory.AddItem(item);
            Wallet.SpendMoney(cost);
        }

        public void DecreaseDurability()
        {
            Inventory.DecreaseDurability();
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            var action = Mathf.FloorToInt(vectorAction[0]);
            PlayerInput.SetAgentAction(this, action);
        }

        private void AddAuctionObs(InventoryItem item)
        {
            bool inAuction = (ChosenScreen == AgentScreen.Auction) && item;

            AddVectorObs(inAuction && gameAuction.IsHighestBidder(this));
            
            AddVectorObs(inAuction ? gameAuction.currentItemPrice : 0.0f);
            AddVectorObs(item, inAuction);
        }

        private void AddVectorObs(InventoryItem item, bool condition=true, float defaultObs=0.0f, bool defaultBool=false)
        {
            AddVectorObs(condition ? item.durability : defaultObs);
            AddVectorObs(condition ? item.efficiency : defaultObs);
            AddVectorObs(condition ? item.unBreakable : defaultBool);
        }

        public override void CollectObservations()
        {
            AddVectorObs((int)ChosenScreen);
            AddVectorObs(Wallet? (float)Wallet.Money : 0.0f);
            AddVectorObs(Item);
            AddVectorObs(gameAuction.ItemCount);
            AddVectorObs(Inventory.ItemCount);
            AddVectorObs(PlayerInput.GetProgress(this));

            AddAuctionObs(gameAuction.auctionedItem);
        }
    }
}