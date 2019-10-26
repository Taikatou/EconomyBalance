using Assets.EconomyProject.Scripts.GameEconomy;
using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using Assets.EconomyProject.Scripts.Inventory;
using Boo.Lang;
using MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents
{
    public enum AgentScreen { Main, Quest, Auction }

    public enum AuctionChoice { Ignore, Bid }

    public class AdventurerAgent : Agent
    {
        public InventoryItem endItem;

        public static int agentCounter = 0;

        [HideInInspector] public int agentId;

        public AgentInventory Inventory => GetComponent<AgentInventory>();

        public InventoryItem Item => Inventory.EquipedItem;

        public bool resetOnComplete = false;

        public bool rewardMoney = false;

        public bool punishLoss = false;

        public bool printObservations = false;

        private List<string> _weaponNames;

        private int GetWeaponName(string weaponName)
        {
            if (_weaponNames == null)
            {
                _weaponNames = new List<string>();
            }

            if (!_weaponNames.Contains(weaponName))
            {
                _weaponNames.Add(weaponName);
            }

            return _weaponNames.IndexOf(weaponName);
        }


        public GameAuction GameAuction => GetComponentInParent<AgentSpawner>().gameAuction;

        public PlayerInput PlayerInput => GetComponentInParent<AgentSpawner>().playerInput;

        public AgentScreen ChosenScreen => PlayerInput.GetScreen(this);

        public EconomyWallet Wallet => GetComponent<EconomyWallet>();

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

            if (resetOnComplete && item && endItem)
            {
                if (item.itemName == endItem.itemName)
                {
                    Done();
                }

                AddReward(item.efficiency / endItem.efficiency);
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
            if (action >= 0)
            {
                PlayerInput.SetAgentAction(this, action);
            }
        }

        private string AddAuctionObs()
        {
            var inAuction = ChosenScreen == AgentScreen.Auction;

            var output = "";
            for (var i = 0; i < 3; i++)
            {
                var item = GameAuction.GetAuctionItem(i);
                AddVectorObs(item, item!=null);
            }

            output += AddVectorObs(inAuction, GameAuction.IsHighestBidder(this), "Is Highest Bidder");

            output += AddVectorObs(inAuction ? GameAuction.currentItemPrice : 0.0f, "Current Price");
            return output;
        }

        protected string AddVectorObs(float observation, string obsName)
        {
            AddVectorObs(observation);
            return " " +  obsName + ": " + observation;
        }

        protected string AddVectorObs(bool valid, bool observation, string obsName)
        {
            if (valid)
            {
                AddVectorObs(observation? 1: 2);
            }
            else
            {
                AddVectorObs(0);
            }
            
            return " " + obsName + ": " + observation;
        }

        private string AddVectorObs(InventoryItem item, bool condition = true, float defaultObs = 0.0f)
        {
            var output = " Current Item";
            output += AddVectorObs(condition ? GetWeaponName(item.itemName) : defaultObs, "Weapon Name Index");
            output += AddVectorObs(condition ? item.durability : defaultObs, "Durability");
            output += AddVectorObs(condition ? item.baseDurability : defaultObs, "Base Durability");
            output += AddVectorObs(condition ? item.numLootSpawns : defaultObs, "Num Loot Spawn");
            output += AddVectorObs(condition ? item.efficiency : defaultObs, "Efficiency");
            output += AddVectorObs(condition && item, item && item.unBreakable, "Unbreakable");

            return output;
        }

        protected string AddVectorObs(AgentScreen observation, string obsName)
        {
            AddVectorObs((int)observation);
            return obsName + ": " + observation;
        }

        public override void CollectObservations()
        {
            var output = AddVectorObs(ChosenScreen, "Chosen Screen");
            output += AddVectorObs(Wallet? (float)Wallet.Money : 0.0f, "Money");
            output += AddVectorObs(Item);
            output += AddVectorObs(GameAuction.ItemCount, "Auction Item Count");
            output += AddVectorObs(Inventory.ItemCount, "Inventory Item Count");
            output += AddVectorObs(PlayerInput.GetProgress(this), "Progress");
            output += AddVectorObs(GameAuction.currentItemPrice, "Current Item Price");


            output += AddAuctionObs();

            if (printObservations)
            {
                Debug.Log(output);
            }
        }

        public void EarnMoney(float amount)
        {
            Wallet.EarnMoney(amount);
            var reward = (amount / endItem.rewardPrice) / 5;
            if (rewardMoney)
            {
                AddReward(reward);
            }
        }

        public void LoseMoney(float amount, float punishment=0.1f)
        {
            if (punishLoss)
            {
                Wallet.LoseMoney(amount);
                if (rewardMoney)
                {
                    AddReward(punishment);
                }
            }
        }
    }
}