using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class GameQuests : EconomySystem
    {
        private readonly float _spawnTime = 3.0f;

        [HideInInspector]
        public float currentTime;

        public override float Progress => currentTime / _spawnTime;

        public GenericLootDropTableGameObject lootDropTable;

        // How many items treasure will spawn
        public int numItemsToDrop = 0;

        public bool finiteMonsters = true;

        private void Start()
        {
            lootDropTable.ValidateTable();
            actionChoice = AgentScreen.Quest;
            currentTime = 0.0f;
        }

        private void Update()
        {
            if(CurrentPlayers.Length > 1)
            {
                currentTime += Time.deltaTime;
                if (currentTime > _spawnTime)
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

                    currentTime = 0.0f;
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
                GameAuction auction = FindObjectOfType<GameAuction>();
                auction.AddAuctionItem(generatedItem);

                Debug.Log(selectedItem.item.Name);
            }

            if (generatedItem != null)
            {
                return generatedItem.baseBidPrice;
            }

            return 0.0f;
        }
    }
}
