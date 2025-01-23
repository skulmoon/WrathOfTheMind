using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemFabric
{
    public Item CreateItem(int id, int count, ItemType type = ItemType.Item)
    {
        Item item = type switch
        {
            ItemType.Item => (Item)ResourceLoader.Load($"res://Data/Resources/Items/{type}s/{id}.tres").Duplicate(true),
            ItemType.Armor => (Armor)ResourceLoader.Load($"res://Data/Resources/Items/{type}s/{id}.tres").Duplicate(true),
            ItemType.Shard => (Shard)ResourceLoader.Load($"res://Data/Resources/Items/{type}s/{id}.tres").Duplicate(true),
            _ => throw new NotImplementedException(),
        };
        if (count > item.MaxCount)
        {
            GD.Print($"Program try create item with count {count}, item initialization with max count. ({item.MaxCount})");
            item.Count = item.MaxCount;
        }
        else
            item.Count = count;
        return item;
    }

    public Item CreateItem(int id, ItemType type) =>
        CreateItem(id, 1, type);
}
