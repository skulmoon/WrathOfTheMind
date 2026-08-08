using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Item : Resource, ICloneable
{
    [Export] public int ID { get; set; } = -1;
    [Export] public int MaxCount { get; set; } = 50;
    [Export] public string Name { get; set; } = "Error";
    [Export] public string Description { get; set; } = "This item is game error.";
    public int Count { get; set; } = 1;

    public Item() { }

    public Item(int id, int maxCount, string itemName, string description)
    {
        ID = id;
        MaxCount = maxCount;
        Name = itemName;
        Description = description;
    }

    public int Staked(int count)
    {
        if ((count + Count) > MaxCount)
        {
            count += Count;
            Count = MaxCount;
            return count - MaxCount;
        }
        else
        {
            Count += count;
            return 0;
        }
    }

    public virtual void UpdateInfo()
    {
        Item newItem = GD.Load<Item>($"res://Data/Resources/Items/Items/{ID}.tres");
        UpdateInfo(newItem);
    }

    public void UpdateInfo(Item newItem)
    {
        MaxCount = newItem.MaxCount;
        Name = newItem.Name;
        Description = newItem.Description;
    }

    public virtual object Clone()
    {
        Item item = new Item(ID, MaxCount, Name, Description);
        item.Count = Count;
        return item;
    }
}