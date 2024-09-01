using Godot;
using System;
using System.Collections.Generic;
public class NPCDialogue
{
    public int NPCID { get; set; }
    public int DialogueNumber { get; set; }
    public List<IDAndText> Speech { get; set; }
    public List<Options> Options { get; set; }
}

public class IDAndText
{
    public string Name { get; set; }
    public string Text { get; set; }
    public string Image { get; set; }
}

public class Options
{
    public int NextDialogue { get; set; }
    public string OptionText { get; set; }
}

public class PlayerChoice
{
    public int NPCID { get; set; }
    public int DialogueNumber { get; set; }
    public int Choice { get; set; }
}

public class GameSettings
{
    public int SaveNumber { get; set; }
    public string CurrentLocation { get; set; } = "Test/Test";
    public Vector2 CurrentTargetPosition { get; set; } = new Vector2(16, 16);
    public Vector2 CurrentPosition { get; set; } = new Vector2(16, 16);
}

public class Save : IComparable<Save>
{
    public string Name { get; set; }
    public int Number { get; set; }

    public int CompareTo(Save compareNumber)
    {
        if (compareNumber == null)
            return 1;

        else
            return this.Number.CompareTo(compareNumber.Number);
    }
}