using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class NPCPAMS : Resource
{
    [Export] public int NPCID { get; set; }
    [Export] public Array<int> DialogueNumber { get; set; }
    [Export] public Array<PAMS> PAMSs { get; set; }
}
