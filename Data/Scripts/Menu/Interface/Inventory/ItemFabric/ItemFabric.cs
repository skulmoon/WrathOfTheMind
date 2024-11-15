using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemFabric
{
    public virtual Item CreateItem(int id, int count)
    {
        Item item = (Item)ResourceLoader.Load<Item>("res://Data/Resources/Items/Items/0.tres").Duplicate(true);
        if (count > item.MaxCount)
        {
            GD.Print($"Program try create item with count {count}, item initialization with max count. ({item.MaxCount})");
            item.Count = item.MaxCount;
        }
        else
            item.Count = count;
        return item;
    }
}
