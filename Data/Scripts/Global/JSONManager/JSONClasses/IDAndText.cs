using Godot;
using System;

[GlobalClass]
public partial class IDAndText : Resource
{
    [Export] public string Name { get; set; }
    [Export] public string Text { get; set; }
    [Export] public string Image { get; set; }
}