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
        Global.Inventory.LoadInventory
        (
            Global.Settings.SaveData.Items,
            Global.Settings.SaveData.Shards,
            Global.Settings.SaveData.Armors,
            Global.Settings.SaveData.Skills
        );
        Global.CutSceneData.LoadCutSceneData();
        Global.SceneObjects.PlayerChanged += SetPlayerSettings;
    }

    public void SetPlayerSettings(Player player)
    {
        player.GlobalPosition = Global.Settings.SaveData.CurrentPosition;
        player.Stamina = Global.Settings.SaveData.Stamina;
        player.HitBox.Health = Global.Settings.SaveData.Health;
        Global.SceneObjects.PlayerChanged -= SetPlayerSettings;
    }

    public void SaveGame()
	{
        if (Global.SceneObjects.Player != null)
        {
            Global.Settings.SaveData.CurrentPosition = Global.SceneObjects.Player?.GlobalPosition ?? new Vector2(160, 400);
            Global.Settings.SaveData.Stamina = Global.SceneObjects.Player.Stamina;
            Global.Settings.SaveData.Health = Global.SceneObjects.Player.HitBox.Health;
        }
        Global.Settings.SaveData.Items = Global.Inventory?.Items ?? Global.Settings.SaveData.Items;
        Global.Settings.SaveData.Armors = Global.Inventory?.Armors ?? Global.Settings.SaveData.Armors;
        Global.Settings.SaveData.Shards = Global.Inventory?.Shards ?? Global.Settings.SaveData.Shards;
        Global.Settings.SaveData.Skills = Global.Inventory?.Skills ?? Global.Settings.SaveData.Skills;
        Global.JSON.SetLocationData(Global.SceneObjects?.Location?.LocationData ?? null);
        Global.CutSceneData.SaveChoices();
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

    public void CreateLocationData() =>
        _directory.CreateLocationData(Global.Settings?.SaveData?.CurrentLocation ?? null);
}
