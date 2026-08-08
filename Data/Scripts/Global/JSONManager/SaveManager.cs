using Godot;
using System;
using System.Collections.Generic;

public partial class SaveManager : Node
{

    private Directory _directory = new Directory();

    public SaveManager()
    {
        Global.Variables.Saves = GetSaves();
        Global.Variables.Saves.Sort();
    }

    public void LoadSave(string save)
	{
        Global.Variables.CurrentSave = save;
        Global.Variables.SaveData = Global.JSON.GetSaveData(save);
        Global.Inventory.LoadInventory
        (
            Global.Variables.SaveData.Items,
            Global.Variables.SaveData.Shards,
            Global.Variables.SaveData.Armors,
            Global.Variables.SaveData.Skills
        );
        Global.CutSceneData.LoadCutSceneData();
        Global.SceneObjects.PlayerChanged += SetPlayerSettings;
    }

    public void SetPlayerSettings(Player player)
    {
        player.GlobalPosition = Global.Variables.SaveData.CurrentPosition;
        player.Stamina = Global.Variables.SaveData.Stamina;
        player.HitBox.Health = Global.Variables.SaveData.Health;
        Global.SceneObjects.PlayerChanged -= SetPlayerSettings;
    }

    public void SaveGame()
	{
        if (Global.SceneObjects.Player != null)
        {
            Global.Variables.SaveData.CurrentPosition = Global.SceneObjects.Player?.GlobalPosition ?? new Vector2(160, 400);
            Global.Variables.SaveData.Stamina = Global.SceneObjects.Player.Stamina;
            Global.Variables.SaveData.Health = Global.SceneObjects.Player.HitBox.Health;
        }
        Global.Variables.SaveData.Items = Global.Inventory?.Items ?? Global.Variables.SaveData.Items;
        Global.Variables.SaveData.Armors = Global.Inventory?.Armors ?? Global.Variables.SaveData.Armors;
        Global.Variables.SaveData.Shards = Global.Inventory?.Shards ?? Global.Variables.SaveData.Shards;
        Global.Variables.SaveData.Skills = Global.Inventory?.Skills ?? Global.Variables.SaveData.Skills;
        Global.JSON.SetLocationData(Global.SceneObjects?.Location?.LocationData ?? null);
        Global.CutSceneData.SaveChoices();
        Global.JSON.SetSaveData(Global.Variables.SaveData);
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
        _directory.CreateLocationData(Global.Variables?.SaveData?.CurrentLocation ?? null);
}
