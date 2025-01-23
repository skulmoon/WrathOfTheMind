using Godot;
using System;

public partial class PAData : Node
{
    public int? NPCID { get; set; }
    public Vector2? StartPosition { get; set; }
    public Vector2? EndPosition { get; set; }
    public string Animation { get; set; }
}
