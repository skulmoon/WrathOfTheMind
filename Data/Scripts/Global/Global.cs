using Godot;
using System;

public partial class Global : Node
{
    public static Settings Settings { get; private set; }
    public static DirectoryManager DirectoryManager { get; private set; }
    public static SceneObjects SceneObjects { get; private set; }
    public static JSONManager JSONManager { get; private set; }
    public static DialogueManager DialogueManager { get; private set; }

    public override void _Ready()
    {
        Settings = new Settings();
        DirectoryManager = new DirectoryManager();
        SceneObjects = new SceneObjects();
        JSONManager = new JSONManager();
        DialogueManager = new DialogueManager();
    }
}
