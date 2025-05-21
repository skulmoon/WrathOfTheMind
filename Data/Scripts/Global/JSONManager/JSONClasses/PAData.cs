using Godot;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;

public partial class PAData : Node
{
    public int? NPCID { get; set; }
    public List<NpcCSData> Data { get; set; }
}
