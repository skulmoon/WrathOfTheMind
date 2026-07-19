using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtention
{
    public static void UpdateItemsInfo(this List<Item> items)
    {
        foreach (Item item in items)
            if (item != null)
                item.UpdateInfo();
    }

    public static List<T> Duplicate<T>(this List<T> list)
    {
        if (!typeof(T).IsAssignableTo(typeof(Node)))
            return null;
        List<T> newList = new List<T>();
        foreach (T element in list)
            if (element is Node node)
                newList.Add((T)Convert.ChangeType(node.Duplicate(), typeof(T)));
        return newList;
    }

    public static List<Shard2D> GetSlaveShards(this List<Shard2D> list) =>
        list.Where(x => !x.IsMain).ToList();
}
