﻿using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class GameQuests : EconomySystem
    {
        private float QuestSuccessProb = 0.9f;

        public List<InventoryItem> items = new List<InventoryItem>();

        private float _spawnTime = 3.0f;

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
                }
                currentTime = 0.0f;
            }
        }

        public void RunQuests(EconomyAgent agent)
        {
            bool questSuccess = Random.value < QuestSuccessProb;
            if(questSuccess)
            {
                float money = GenerateItem(0);
                agent.EarnMoney(money);
            }
        }

        private float GenerateItem(int damage)
        {
            System.Random rand = new System.Random();

            // Generate a random index less than the size of the array.  
            int index = rand.Next(items.Count);

            InventoryItem generatedItem = ScriptableObject.CreateInstance("InventoryItem") as InventoryItem;

            generatedItem?.Init(items[index]);

            GameAuction auction = FindObjectOfType<GameAuction>();
            auction.AddAuctionItem(generatedItem);

            return generatedItem.baseBidPrice;
        }
    }
}
