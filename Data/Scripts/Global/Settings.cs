using Godot;
using System;
using System.Collections.Generic;

public class Settings
{
    public int GridSize { get; private set; } = 32;
    public bool CutScene { get; set; } = false;
    public string CurrentSave { get; set; }
    public List<Save> Saves { get; set; }
    public SaveData SaveData { get; set; }
}
