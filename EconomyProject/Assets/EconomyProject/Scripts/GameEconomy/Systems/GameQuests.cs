using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.Inventory.LootBoxes.Generated;
using Assets.EconomyProject.Scripts.MLAgents.EconomyAgentsAgent;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy.Systems
{
    public class GameQuests : EconomySystem
    {
        public float spawnTime = 3.0f;

        private float _currentTime;

        public override float Progress => _currentTime / spawnTime;

        public GenericLootDropTableGameObject lootDropTable;

        // How many items treasure will spawn
        public int numItemsToDrop = 0;

        public bool finiteMonsters = true;

        public bool autoReturn = false;

        private bool _shouldReturn;

        protected override AgentScreen ActionChoice => AgentScreen.Quest;

        public GameAuction auction;

        public override bool CanMove(EconomyAgent agent)
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
            if(CurrentPlayers.Length >= 1)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime > spawnTime)
                {
                    if (finiteMonsters)
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
        }

        public void RunQuests(EconomyAgent agent)
        {
            bool questSuccess = Random.value < (agent.Item.efficiency / 100);
            if(questSuccess)
            {
                float money = GenerateItem(0);
                agent.EarnMoney(money);
            }
            agent.DecreaseDurability();
        }

        private float GenerateItem(int damage)
        {
            InventoryItem generatedItem = null;

            for (int i = 0; i < numItemsToDrop; i++)
            {
                GeneratedLootItemScriptableObject selectedItem = lootDropTable.PickLootDropItem();
                generatedItem = ScriptableObject.CreateInstance("InventoryItem") as InventoryItem;

                generatedItem?.Init(selectedItem.item);
                
                auction.AddAuctionItem(generatedItem);
            }

            if (generatedItem != null)
            {
                return generatedItem.rewardPrice;
            }

            return 0.0f;
        }
    }
}
