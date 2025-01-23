using Godot;
using System;
using System.Collections.Generic;

public class PlayerSettings
{
    public List<Item> Items { get; set; }
    public List<Item> Armors { get; set; }
    public List<Item> Shards { get; set; }
    public int Scruples { get; set; }
    public Vector2 CurrentTargetPosition { get; set; } = new Vector2(16, 16);
    public Vector2 CurrentPosition { get; set; } = new Vector2(16, 16);
}
