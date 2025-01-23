using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerInventory : Node
{
    public List<Item> Items { get; set; } = new List<Item>(new Item[33]);
    public List<Item> Armors { get; set; } = new List<Item>(new Item[21]);
    public List<Item> Shards { get; set; } = new List<Item>(new Item[21]);
    public int Scruples { get; set; } = 0;
    public event Action ShardChanged = () => { };

    public PlayerInventory()
    {
        for (int i = 0; i < Items.Count; i++)
            Items[i] = Global.ItemFabric.CreateItem(0, 12);
        for (int i = 16; i < 20; i++)
            Shards[i] = Global.ItemFabric.CreateItem(1, ItemType.Shard);
    }

    public bool TakeItem(Item item)
	{
        List<Item> list = null;
        if (item is Armor)
            list = Armors;
        else if (item is Shard)
            list = Shards;
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                {
                    list[i] = item;
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
                item.Count = Items[i].Staked(item.Count);
                if (item.Count == 0)
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
        List<Item> list = null;
        switch (type)
        {
            case ItemType.Item:
                list = Items;
                break;
            case ItemType.Armor:
                list = Armors;
                break;
            case ItemType.Shard:
                list = Shards;
                break;
        }
        (list[endPosition], list[startPosition]) = (list[startPosition], list[endPosition]);
        if (type == ItemType.Shard && ((startPosition > 15 && startPosition != 20) || (endPosition > 15 && endPosition != 20)))
            ShardChanged.Invoke();
    }
}
