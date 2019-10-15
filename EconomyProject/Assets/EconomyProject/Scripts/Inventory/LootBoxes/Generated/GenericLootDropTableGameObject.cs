﻿using System.Linq;

namespace Assets.EconomyProject.Scripts.Inventory.LootBoxes.Generated
{
    [System.Serializable]
    public class GenericLootDropTableGameObject : GenericLootDropTable<GeneratedLootItemScriptableObject, InventoryItem>
    {
        public float MaxMoney => lootDropItems.Max(x => x.item.baseBidPrice);
    }
}
