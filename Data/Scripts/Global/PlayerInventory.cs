using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerInventory : Node
{
    public List<Item> Items { get; set; } = new List<Item>(new Item[25]);
    public List<Item> Weapons { get; set; } = new List<Item>(new Item[9]);
    public List<Item> Shards { get; set; } = new List<Item>(new Item[9]);
	public int Scruples { get; set; } = 0;

    public PlayerInventory()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            var item = Global.ItemFabric.CreateItem(0, 12);
            Items[i] = item;
        }
    }

    public bool TakeItem(Item item)
	{
        if (item is Armor)
        {
            for (int i = 0; i < Weapons.Count; i++)
            {
                if (Weapons[i] == null)
                {
                    Weapons[i] = item;
                    return true;
                }
            }
            return false;
        }
        if (item is Shard)
        {
            for (int i = 0; i < Shards.Count; i++)
            {
                if (Shards[i] == null)
                {
                    Shards[i] = item;
                    return true;
                }
            }
            return false;
        }
        int? freeNumber = null;
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].ID == item.ID)
            {
                Items[i].Count += item.Count;
                return true;
            }
            else if ((freeNumber == null) && (Items[i] == null))
                freeNumber = i;
        }
        if (freeNumber != null)
        {
            Items[(int)freeNumber] = item;
            return true;
        }
        return false;
    }

    public void MovingItem(ItemType type, int startPosition, int endPosition)
    {
        switch (type)
        {
            case ItemType.Item:
            {
                Item item = Items[startPosition];
                Items[startPosition] = Items[endPosition];
                Items[endPosition] = item;
                break;
            }
            case ItemType.Armor:
            {
                Item item = Weapons[startPosition];
                Weapons[startPosition] = Weapons[endPosition];
                Weapons[endPosition] = item;
                break;
            }
            case ItemType.Shard:
            {
                Item item = Shards[startPosition];
                Shards[startPosition] = Shards[endPosition];
                Shards[endPosition] = item;
                break;
            }
        }
    }
}

public enum ItemType
{
    Item,
    Armor, 
    Shard
}