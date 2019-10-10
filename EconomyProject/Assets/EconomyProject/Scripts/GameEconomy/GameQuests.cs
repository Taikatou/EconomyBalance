using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public struct QuestItems
    {
        InventoryItem item;
        float Probability;
    }

    public class GameQuests : EconomySystem
    {
        public List<InventoryItem> items = new List<InventoryItem>();

        private float _spawnTime = 3.0f;

        [HideInInspector]
        public float currentTime;

        public float Progress => currentTime / _spawnTime;

        private void Start()
        {
            actionChoice = AgentScreen.Quest;
            currentTime = 0.0f;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if(currentTime > _spawnTime)
            {
                foreach (var agent in CurrentPlayers)
                {
                    RunQuests(agent);
                    agent.DecreaseDurability();
                }
                currentTime = 0.0f;
            }
        }

        public void RunQuests(EconomyAgent agent)
        {
            bool questSuccess = Random.value < (agent.Item.Efficiency / 100);
            Debug.Log((agent.Item.Efficiency / 100));
            if(questSuccess)
            {
                float money = GenerateItem(0, agent.Item.MaxRarityType);
                agent.EarnMoney(money);
            }
        }

        private float GenerateItem(int damage, RarityTypes maxRarity)
        {
            InventoryItem generatedItem;
            do
            {
                System.Random rand = new System.Random();

                // Generate a random index less than the size of the array.  
                int index = rand.Next(items.Count);

                generatedItem = ScriptableObject.CreateInstance("InventoryItem") as InventoryItem;

                generatedItem?.Init(items[index]);

                GameAuction auction = FindObjectOfType<GameAuction>();
                auction.AddAuctionItem(generatedItem);
            }
            while (generatedItem && generatedItem.RarityType <= maxRarity);
            return generatedItem.baseBidPrice;
        }
    }
}
