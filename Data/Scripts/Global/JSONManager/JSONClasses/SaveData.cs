using Godot;
using System;
using System.Collections.Generic;

public class SaveData
{
    public List<Item> Items { get; set; }
    public List<Item> Armors { get; set; }
    public List<Item> Shards { get; set; }
    public List<Item> Skills { get; set; }
    public int Scruples { get; set; }
    public int SaveNumber { get; set; }
    public Vector2 CurrentPosition { get; set; } = new Vector2(160, 400);
    public int Health { get; set; } = 100;
    public float Stamina { get; set; } = 100;
    public string CurrentLocation { get; set; } = "Test/Test";
}
