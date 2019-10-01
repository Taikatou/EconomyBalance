using UnityEngine;

[CreateAssetMenu]
public class InventoryItem : ScriptableObject
{
    public readonly float BaseBidPrice;

    public readonly string Name;

    public readonly int Dmg;

    public InventoryItem(float baseBidPrice, string name, int dmg)
    {
        BaseBidPrice = baseBidPrice;
        Name = name;
        Dmg = dmg;
    }

    public InventoryItem(InventoryItem item) : this(item.BaseBidPrice, item.Name, item.Dmg)
    {
        
    }
}
