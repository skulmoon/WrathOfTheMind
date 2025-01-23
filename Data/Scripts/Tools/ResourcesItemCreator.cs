using Godot;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


[Tool]
public partial class ResourcesItemCreator : Node
{
    [Export] public int ID { get; set; }
    [Export] public int MaxCount { get; set; }
    [Export] public string ItemName { get; set; }
    [Export] public string Description { get; set; }
    [Export] public virtual bool Create 
    { 
        get => default;
        set
        {
            if (!CheckID(ItemType.Item))
                return;
            Item item;
            if (ItemName != null && Description != null)
            {
                item = new Item(ID, MaxCount, ItemName, Description);
                ResourceSaver.Save(item, "res://Data/Resources/Items/Items/" + item.ID + ".tres");
            }
        }
    }

    public bool CheckID(ItemType Type)
    {
        List<string> result = new List<string>();
        switch (Type)
        {
            case ItemType.Item:
                result = System.IO.Directory.GetFiles(ProjectSettings.GlobalizePath("res://Data/Resources/Items/Items/")).ToList();
                break;
            case ItemType.Armor:
                result = System.IO.Directory.GetFiles(ProjectSettings.GlobalizePath("res://Data/Resources/Items/Armors/")).ToList();
                break;
            case ItemType.Shard:
                result = System.IO.Directory.GetFiles(ProjectSettings.GlobalizePath("res://Data/Resources/Items/Shards/")).ToList();
                break;
        }
        for (int i = 0; i < result.Count; i++)
        {
            string buffer = Path.GetFileName(result[i]);
            result[i] = "";
            for (int j = 0; j < buffer.Length; j++)
            {
                if (buffer[j] == '.')
                    break;
                result[i] += buffer[j];
            }
        }
        for (int i = 0; i < result.Count; i++)
            if (ID.ToString() == result[i])
            {
                GD.Print("Item already exists.");
                return false;
            }
        return true;
    }
}
