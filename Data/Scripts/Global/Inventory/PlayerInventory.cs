using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerInventory : Node
{
    public List<Item> Items { get; set; } = new List<Item>(new Item[33]);
    public int ItemsCount { get; set; } = 16;
    public List<Item> Armors { get; set; } = new List<Item>(new Item[18]);
    public int ArmorsCount { get; set; } = 8;
    public List<Item> Shards { get; set; } = new List<Item>(new Item[21]);
    public int ShardsCount { get; set; } = 8;
    public int Scruples { get; set; } = 180;
    public event Action<List<Shard>> ShardsChanged;
    public event Action<Armor> ArmorChanged;

    public bool TakeItem(Item item)
	{
        List<Item> list = null;
        InventoryItems inventoryItems;
        int count = 0;
        if (item is Armor)
        {
            list = Armors;
            inventoryItems = Global.SceneObjects.InventoryMenu.GetNode<InventoryItems>("Armors");
            count = ArmorsCount;
        }
        else if (item is Shard)
        {
            list = Shards;
            inventoryItems = Global.SceneObjects.InventoryMenu.GetNode<InventoryItems>("Shards");
            count = ShardsCount;
        }
        else
            inventoryItems = Global.SceneObjects.InventoryMenu.GetNode<InventoryItems>("Items");
        if (list != null)
        {
            for (int i = 0; i < count; i++)
            {
                if (list[i] == null)
                {
                    list[i] = item;
                    inventoryItems.Cells.Find(x => x.ItemNumber == i).UpdateItem();
                    return true;
                }
            }
            return false;
        }
        int? freeNumber = null;
        for (int i = 0; i < ItemsCount; i++)
        {
            if (Items[i]?.ID == item.ID) 
            {
                item.Count = Items[i].Staked(item.Count);
                if (item.Count == 0)
                {
                    inventoryItems.Cells.Find(x => x.Item == Items[i]).UpdateItem();
                    return true;
                }
            }
            else if ((freeNumber == null) && (Items[i] == null))
                freeNumber = i;
        }
        if (freeNumber != null)
        {
            Items[(int)freeNumber] = item;
            inventoryItems.Cells.Find(x => x.ItemNumber == (int)freeNumber).UpdateItem();
            return true;
        }
        return false;
    }

    public void MovingItem(ItemType type, int startPosition, int endPosition)
    {
        List<Item> list = null;
        list = type.GetList();
        (list[endPosition], list[startPosition]) = (list[startPosition], list[endPosition]);
        if (type == ItemType.Shard && ((startPosition > 15 && startPosition != 20) || (endPosition > 15 && endPosition != 20)))
            ShardsChanged?.Invoke([(Shard)list[16], (Shard)list[17], (Shard)list[18], (Shard)list[19]]);
        if (type == ItemType.Armor && (startPosition == 16 || endPosition == 16))
            ArmorChanged?.Invoke((Armor)list[16]);
    }

    public List<Shard> GetActiveShardList() =>
        [(Shard)Shards[16], (Shard)Shards[17], (Shard)Shards[18], (Shard)Shards[19]];

    public Armor GetActiveArmor() =>
        (Armor)Armors[16];

    public void Clear()
    {
        for (int i = 0; i < Armors.Count; i++)
            Armors[i] = null;
        for (int i = 0; i < Shards.Count; i++)
            Shards[i] = null;
        for (int i = 0; i < Items.Count; i++)
            Items[i] = null;
    }
}

