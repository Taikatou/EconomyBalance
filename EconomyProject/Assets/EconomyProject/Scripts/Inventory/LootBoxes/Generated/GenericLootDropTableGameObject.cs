using Assets.EconomyProject.Scripts.Inventory;
using System.Linq;

[System.Serializable]
public class GenericLootDropTableGameObject : GenericLootDropTable<GeneratedLootItemScriptableObject, InventoryItem>
{
    public float MaxMoney => lootDropItems.Max(x => x.item.baseBidPrice);
}
