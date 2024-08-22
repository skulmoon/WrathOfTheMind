using Godot;
using System;

public partial class Settings : Node
{
    public int GridSize { get; private set; } = 32;
    public string ScenePath { get; set; } = "/root/Test/";
    public Player Player { get; set; }
    public bool CutScene { get; set; } = false;
}
