using System.Collections.Generic;
using UnityEngine;

public class GameQuests : EconomySystem
{
    private float QuestSuccessProb = 0.9f;

    public bool RanQuests = false;

    public List<InventoryItem> Items = new List<InventoryItem>();

    private void Start()
    {
        actionChoice = AgentActionChoice.Quest;
    }

    public bool RunQuests()
    {
        if (!RanQuests)
        {
            foreach (var agent in CurrentPlayers)
            {
                bool questSuccess = Random.value < QuestSuccessProb;
                if (questSuccess)
                {
                    int damage = agent.GetComponent<AgentInventory>().Damage;
                    QuestSuccess(damage);
                }
            }
            RanQuests = true;
        }
        return RanQuests;
    }

    private void QuestSuccess(int Damage)
    {
        InventoryItem generatedItem = null;
        while(!generatedItem && (generatedItem.Dmg > Damage))
        {
            System.Random rand = new System.Random();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(Items.Count);
            generatedItem = new InventoryItem(Items[index]);
        }
        
        GameAuction auction = GetComponent<GameAuction>();
        auction.AddAuctionItem(generatedItem);
    }

    public void Reset()
    {
        RanQuests = false;
    }
}
