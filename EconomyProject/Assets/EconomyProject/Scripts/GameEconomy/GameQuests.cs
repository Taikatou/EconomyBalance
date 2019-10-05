using System.Collections.Generic;
using Assets.EconomyProject.Scripts.Inventory;
using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.GameEconomy
{
    public class GameQuests : EconomySystem
    {
        private float QuestSuccessProb = 0.9f;

        public bool ranQuests;

        public List<InventoryItem> items = new List<InventoryItem>();

        private void Start()
        {
            actionChoice = AgentActionChoice.Quest;
        }

        public bool RunQuests()
        {
            if (!ranQuests)
            {
                foreach (var agent in CurrentPlayers)
                {
                    bool questSuccess = Random.value < QuestSuccessProb;
                    if (true)
                    {
                        QuestSuccess(0);
                    }
                }
                ranQuests = true;
            }
            return ranQuests;
        }

        private void QuestSuccess(int damage)
        {
            InventoryItem generatedItem = null;
            System.Random rand = new System.Random();

            // Generate a random index less than the size of the array.  
            int index = rand.Next(items.Count);
            generatedItem = new InventoryItem(items[index]);

            GameAuction auction = GetComponent<GameAuction>();
            auction.AddAuctionItem(generatedItem);
        }

        public void Reset()
        {
            ranQuests = false;
        }
    }
}
