using Godot;
using System;

[GlobalClass]
public partial class NPCDialogueParams : Resource
{
    [Export] public int WeirdPoints { get; set; }
    [Export] public int ReputationPoints { get; set; }
}
