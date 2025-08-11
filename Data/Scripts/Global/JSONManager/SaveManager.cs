using Godot;
using System;
using System.Collections.Generic;

public partial class SaveManager : Node
{

    private Directory _directory = new Directory();

    public SaveManager()
    {
        Global.Settings.Saves = GetSaves();
        Global.Settings.Saves.Sort();
    }

    public void LoadSave(string save)
	{
        Global.Settings.CurrentSave = save;
        Global.Settings.SaveData = Global.JSON.GetSaveData(save);
        Global.CutSceneData.LoadCutSceneData();
        Global.SceneObjects.PlayerChanged += SetPlayerSettings;
    }

    public void SetPlayerSettings(Node player)
    {
        ((Player)player).GlobalPosition = Global.Settings.SaveData.CurrentPosition;
        Global.Inventory.Items = Global.Settings.SaveData.Items;
        Global.Inventory.Shards = Global.Settings.SaveData.Shards;
        Global.Inventory.Armors = Global.Settings.SaveData.Armors;
        ((Player)player).Shard.UpdateShard();
        Global.SceneObjects.PlayerChanged -= SetPlayerSettings;
    }

    public void SaveGame()
	{
        Global.Settings.SaveData.CurrentPosition = Global.SceneObjects.Player?.GlobalPosition ?? new Vector2(160, 400);
        Global.Settings.SaveData.Items = Global.Inventory?.Items ?? Global.Settings.SaveData.Items;
        Global.Settings.SaveData.Armors = Global.Inventory?.Armors ?? Global.Settings.SaveData.Armors;
        Global.Settings.SaveData.Shards = Global.Inventory?.Shards ?? Global.Settings.SaveData.Shards;
        Global.JSON.SetSaveData(Global.Settings.SaveData);
    }

    public void NewGame(string saveName, int saveNumber)
    {
        _directory.CreateSave(saveName, saveNumber);
        SaveGame();
        Global.CutSceneData.LoadCutSceneData();
    }

    public List<Save> GetSaves()
    {
        List<string> saves = _directory.GetSaveNames();
        List<Save> result = new List<Save>();
        for (int i = 0; i < saves.Count; i++)
        {
            Save save = new Save();
            save.Name = saves[i];
            save.Number = Global.JSON.GetSaveData(saves[i])?.SaveNumber ?? 0;
            result.Add(save);
        }
        return result;
    }

    public void DeleteSave(string saveName) =>
        _directory.DeleteSave(saveName);
}
