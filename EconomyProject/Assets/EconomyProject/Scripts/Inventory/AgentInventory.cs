using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentInventory : MonoBehaviour
{
    private List<InventoryItem> _items;
    void Start()
    {
        _items = new List<InventoryItem>();
    }

    public void AddItem(InventoryItem item)
    {
        _items.Add(item);
    }

    public InventoryItem EquipedItem
    {
        get
        {
            var max = _items.Max(x => x.Dmg);
            var maxWeapon = _items.First(x => x.Dmg == max);
            return maxWeapon;
        }
    }

    public int Damage => EquipedItem.Dmg;
}
