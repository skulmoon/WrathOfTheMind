using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class NPCDialogue : Resource
{
    [Export] public int NPCID { get; set; }
    [Export] public int DialogueNumber { get; set; }
    [Export] public Array<IDAndText> Speech { get; set; }
    [Export] public Array<Option> Options { get; set; }
}