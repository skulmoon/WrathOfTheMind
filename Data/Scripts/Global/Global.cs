using Godot;
using System;

public partial class Global : Node
{
    public static Settings Settings { get; private set; }
    public static Directory Directory { get; private set; }
    public static JSON JSON { get; private set; }
    public static SceneObjects SceneObjects { get; private set; }
    public static DialogueManager Dialogue { get; private set; }
    public static ItemFabric ItemFabric { get; private set; }

    public override void _Ready()
    {
        Settings = new Settings();
        Directory = new Directory();
        SceneObjects = new SceneObjects();
        JSON = new JSON();
        Dialogue = new DialogueManager();
        ItemFabric = new ItemFabric();
    }
}
