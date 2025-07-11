using Godot;
using System;

public partial class Global : Node
{
    public static Settings Settings { get; private set; }
    public static JSON JSON { get; private set; }
    public static SceneObjects SceneObjects { get; private set; }
    public static SaveManager SaveManager { get; private set; }
    public static CutSceneData CutSceneData { get; private set; }
    public static CutSceneManager CutSceneManager { get; private set; }
    public static ItemFabric ItemFabric { get; private set; }
    public static Sound Music { get; private set; }
    public static PlayerInventory Inventory { get; private set; }

    public Global()
    {
        ProcessPriority = 10;
        Settings = new Settings();
        SceneObjects = new SceneObjects();
        JSON = new JSON();
        SaveManager = new SaveManager();
        CutSceneData = new CutSceneData();
        CutSceneManager = new CutSceneManager();
        ItemFabric = new ItemFabric();
        Music = new Sound();
        Inventory = new PlayerInventory();
    }
}
