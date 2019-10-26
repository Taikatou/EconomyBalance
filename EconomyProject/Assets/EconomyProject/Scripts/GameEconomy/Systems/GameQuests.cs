using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.Inventory.LootBoxes.Generated;
using Assets.EconomyProject.Scripts.MLAgents.AdventurerAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems
{
    public class GameQuests : EconomySystem
    {
        public float spawnTime = 3.0f;

        private float _currentTime;

        public override float Progress => _currentTime / spawnTime;

        public GenericLootDropTableGameObject lootDropTable;

        public bool finiteMonsters = true;

        public bool autoReturn = false;

        private bool _shouldReturn;

        protected override AgentScreen ActionChoice => AgentScreen.Quest;

        public GameAuction auction;

        public bool multipleLootDrops = true;

        public bool punishFailure = true;

        public override bool CanMove(AdventurerAgent agent)
        {
            return !_shouldReturn;
        }

        private void Start()
        {
            lootDropTable.ValidateTable();
            _currentTime = 0.0f;
            _shouldReturn = autoReturn;
        }

        private void Update()
        {
            if (CurrentPlayers.Length >= 1)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime > spawnTime)
                {
                    if (!finiteMonsters)
                    {
                        foreach (var agent in CurrentPlayers)
                        {
                            RunQuests(agent);
                        }
                    }
                    else
                    {
                        System.Random random = new System.Random();
                        int start2 = random.Next(0, CurrentPlayers.Length - 1);
                        var agent = CurrentPlayers[start2];

                        RunQuests(agent);
                    }

                    if (autoReturn)
                    {
                        _shouldReturn = false;
                        foreach (var agent in CurrentPlayers)
                        {
                            playerInput.SetMainAction(agent, AgentScreen.Main);
                        }
                        _shouldReturn = true;
                    }
                    else
                    {
                        RequestDecisions();
                    }

                    _currentTime = 0.0f;
                }
            }
            RequestDecisions();
        }

        public void RunQuests(AdventurerAgent agent)
        {
            
            bool questSuccess = Random.value < (agent.Item.efficiency / 100);
            if(questSuccess)
            {
                float money = GenerateItem(agent.Item) / 2;
                agent.EarnMoney(money);
            }
            else if (punishFailure)
            {
                agent.LoseMoney(2);
            }
            agent.DecreaseDurability();
        }

        private float GenerateItem(InventoryItem attackWeapon)
        {
            float totalPrice = 0.0f;

            System.Random r = new System.Random();
            int numSpawns = r.Next(1, attackWeapon.numLootSpawns);
            int dropped = 0;
            for (int i = 0; i < numSpawns && (multipleLootDrops || i==0); i++)
            {
                GeneratedLootItemScriptableObject selectedItem = lootDropTable.PickLootDropItem();
                var generatedItem = ScriptableObject.CreateInstance("InventoryItem") as InventoryItem;
                dropped++;
                generatedItem?.Init(selectedItem.item);
                
                auction.AddAuctionItem(generatedItem);

                if (generatedItem != null)
                {
                    totalPrice += generatedItem.rewardPrice;
                }
            }
            return totalPrice;
        }
    }
}
