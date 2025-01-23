using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public static class ItemTypeExtension
{
    public static List<Item> GetList(this ItemType type)
    {
        return type switch
        {
            ItemType.Item => Global.SceneObjects.Player.Inventory.Items,
            ItemType.Armor => Global.SceneObjects.Player.Inventory.Armors,
            ItemType.Shard => Global.SceneObjects.Player.Inventory.Shards,
            _ => null,
        };
    }

    public static Type GetClass(this ItemType type)
    {
        return type switch
        {
            ItemType.Item => typeof(Item),
            ItemType.Armor => typeof(Armor),
            ItemType.Shard => typeof(Shard),
            _ => null,
        };
    }
}
