using Godot;
using System;

public partial class NpcCSData : Node
{
    public Vector2 Position { get; set; }
    public string Animation { get; set; }
    public float? Time { get; set; }
    public int? Customize { get; set; }
    public string Sound { get; set; }
}
