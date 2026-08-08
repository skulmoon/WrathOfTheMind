using Godot;
using System;

[GlobalClass]
public partial class Option : Resource
{
    [Export] public int NextDialogue { get; set; }
    [Export] public string OptionText { get; set; }
}
