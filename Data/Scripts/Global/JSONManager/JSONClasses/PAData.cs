using Godot;
using System;
using System.Collections.Generic;

public partial class PAData : Node
{
    public int? NPCID { get; set; }
    public List<(Vector2 Position, string Animation, float? Time, int? Customize)> Data { get; set; }
}
