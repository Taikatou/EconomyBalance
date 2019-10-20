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
        public InventoryItem endItem;

        public static int agentCounter = 0;

        [HideInInspector] public int agentId;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

        public InventoryItem Item => Inventory.EquipedItem;

        public GameAuction GameAuction => GetComponentInParent<AgentSpawner>().gameAuction;

        public PlayerInput PlayerInput => GetComponentInParent<AgentSpawner>().playerInput;

        public AgentScreen ChosenScreen => PlayerInput.GetScreen(this);

        public EconomyWallet Wallet => GetComponent<EconomyWallet>();

        public bool printInput = false;

        public override void AgentReset()
        {
            var reset = GetComponentInParent<ResetScript>();
            reset.Reset();
        }

        public void ResetEconomyAgent()
        {
            Inventory.ResetInventory();
            Wallet.Reset();
        }

        public override void InitializeAgent()
        {
            agentId = agentCounter;
            agentCounter++;
        }

        public void BoughtItem(InventoryItem item, float cost)
        {
            Inventory.AddItem(item);
            Wallet?.SpendMoney(cost);

            if (item && endItem)
            {
                if (item.itemName == endItem.itemName)
                {
                    Done();
                }
            }
        }

        public void DecreaseDurability()
        {
            Inventory.DecreaseDurability();
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            var action = Mathf.FloorToInt(vectorAction[0]);
            AgentAction(action);
        }

        public virtual void AgentAction(int action)
        {
            if (printInput)
            {
                Debug.Log(action);
            }

            if (action >= 0)
            {
                PlayerInput.SetAgentAction(this, action);
            }
        }

        private void AddAuctionObs(InventoryItem item)
        {
            bool inAuction = (ChosenScreen == AgentScreen.Auction) && item;

            AddVectorObs(inAuction && GameAuction.IsHighestBidder(this));
            
            AddVectorObs(inAuction ? GameAuction.currentItemPrice : 0.0f);
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
            AddVectorObs(GameAuction.ItemCount);
            AddVectorObs(Inventory.ItemCount);
            AddVectorObs(PlayerInput.GetProgress(this));

            AddAuctionObs(GameAuction.auctionedItem);
        }

        public void EarnMoney(float amount)
        {
            Wallet.EarnMoney(amount);
            var reward = amount / endItem.rewardPrice;
            Debug.Log("Reward: " + reward);
            AddReward((float)reward);
        }
    }
}