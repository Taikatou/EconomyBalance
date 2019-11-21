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

        public GenericLootDropTableGameObject lootDropTable;

        public bool finiteMonsters = true;

        public bool autoReturn = false;

        private bool _shouldReturn;

        public GameAuction auction;

        public bool multipleLootDrops = true;

        public bool punishFailure = true;

        public override float Progress => _currentTime / spawnTime;

        protected override AgentScreen ActionChoice => AgentScreen.Quest;

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

        private void FixedUpdate()
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
                        var random = new System.Random();
                        var start2 = random.Next(0, CurrentPlayers.Length - 1);
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

                    _currentTime = 0.0f;
                }
            }
        }

        public void RunQuests(AdventurerAgent agent)
        {
            var questSuccess = Random.value < (agent.Item.efficiency / 100);
            if(questSuccess)
            {
                var money = GenerateItem(agent.Item) / 2;
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
            var totalPrice = 0.0f;

            var r = new System.Random();
            var numSpawns = r.Next(1, attackWeapon.numLootSpawns);
            for (var i = 0; i < numSpawns && (multipleLootDrops || i==0); i++)
            {
                var selectedItem = lootDropTable.PickLootDropItem();
                var generatedItem = InventoryItem.GenerateItem(selectedItem.item);
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
