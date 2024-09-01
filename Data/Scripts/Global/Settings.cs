using Godot;
using System;
using System.Collections.Generic;

public class Settings
{
    public int GridSize { get; private set; } = 32;
    public int SaveNumber { get; set; }
    public string ScenePath { get; set; }
    public bool CutScene { get; set; }
    public string CurrentSave { get; set; }
    public string CurrentLocation { get; set; }
    public Vector2 CurrentTargetPosition { get; set; }
    public Vector2 CurrentPosition { get; set; }
    public List<Save> Saves { get; set; }
}
