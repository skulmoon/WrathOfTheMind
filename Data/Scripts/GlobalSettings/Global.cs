using Godot;
using System;

public partial class Global : Node
{
    public static DialogueManager DialogueManager { get; private set; }
    public static Settings Settings { get; private set; }

    public override void _Ready()
    {
        DialogueManager = new DialogueManager();
        Settings = new Settings();
    }
}
