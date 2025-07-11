using Godot;
using System;
using System.Collections.Generic;

public class SaveData
{
    public List<Item> Items { get; set; }
    public List<Item> Armors { get; set; }
    public List<Item> Shards { get; set; }
    public int Scruples { get; set; }
    public int SaveNumber { get; set; }
    public Vector2 CurrentPosition { get; set; } = new Vector2(160, 400);
    public string CurrentLocation { get; set; } = "Test/Test";
}
