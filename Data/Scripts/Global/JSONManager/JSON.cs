using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class JSON
{
    private string _pathDialogues = "res://Data/Dialogs/";
    private string _pathPams = "res://Data/PAMS/";
    private string _pathChoices = "user://Saves/";
    private Directory _directory = new Directory();
    private ConfigLoader _config = new ConfigLoader();

    public ConfigInfo ConfigInfo { get => _config.ConfigInfo; }

    public JSON()
    {
        Global.Settings.Saves = GetSaves();
        Global.Settings.Saves.Sort();
    }

    public List<NPCPAMS> GetNpcpams()
    {
        FileAccess file = FileAccess.Open($"{_pathPams}{Global.Settings.GameSettings.CurrentLocation}.json", FileAccess.ModeFlags.Read);
        string json = file.GetAsText() ?? "";
        file.Close();
        return JsonConvert.DeserializeObject<List<NPCPAMS>>(json);
    }

    public List<NPCDialogue> GetDialogues()
    {
        FileAccess file = FileAccess.Open($"{_pathDialogues}{Global.Settings.GameSettings.CurrentLocation}.json", FileAccess.ModeFlags.Read);
        string json = file.GetAsText() ?? "";
        file.Close();
        return JsonConvert.DeserializeObject<List<NPCDialogue>>(json);
    }

    public List<PlayerChoice> GetPlayerChoices()
    {
        FileAccess file = FileAccess.Open($"{_pathChoices}{Global.Settings.CurrentSave}/Choices/{Global.Settings.GameSettings.CurrentLocation}.json", FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();
        return JsonConvert.DeserializeObject<List<PlayerChoice>>(json);
    }

    public void SetPlayerChoices(List<PlayerChoice> playerChoices)
    {
        FileAccess file = FileAccess.Open($"{_pathChoices}{Global.Settings.CurrentSave}/Choices/{Global.Settings.GameSettings.CurrentLocation}.json", FileAccess.ModeFlags.Write);
        string json = JsonConvert.SerializeObject(playerChoices, Formatting.Indented);
        file.StoreString(json);
        file.Close();
    }

    public void SaveGame()
    {
        FileAccess gameFile = FileAccess.Open($"{_pathChoices}{Global.Settings.CurrentSave}/GameSettings.json", FileAccess.ModeFlags.Write);
        FileAccess playerFile = FileAccess.Open($"{_pathChoices}{Global.Settings.CurrentSave}/PlayerSettings.json", FileAccess.ModeFlags.Write);
        Global.Settings.PlayerSettings.CurrentPosition = Global.SceneObjects.Player?.Position ?? new Vector2(16,16);
        Global.Settings.PlayerSettings.CurrentTargetPosition = Global.SceneObjects.Player?.TargetPosition ?? new Vector2(16, 16);
        Global.Settings.PlayerSettings.Items = Global.SceneObjects.Player?.Inventory?.Items ?? Global.Settings.PlayerSettings.Items;
        Global.Settings.PlayerSettings.Armors = Global.SceneObjects.Player?.Inventory?.Armors ?? Global.Settings.PlayerSettings.Armors;
        Global.Settings.PlayerSettings.Shards = Global.SceneObjects.Player?.Inventory?.Shards ?? Global.Settings.PlayerSettings.Shards;
        Global.SceneObjects.ResetPLayer();
        string gameJson = JsonConvert.SerializeObject(Global.Settings.GameSettings, Formatting.Indented);
        string playerJson = JsonConvert.SerializeObject(Global.Settings.PlayerSettings, Formatting.Indented);
        gameFile.StoreString(gameJson);
        playerFile.StoreString(playerJson);
        gameFile.Close();
        playerFile.Close();
    }

    public void LoadGame(string save)
    {
        Global.Settings.CurrentSave = save;
        FileAccess gameFile = FileAccess.Open($"{_pathChoices}{save}/GameSettings.json", FileAccess.ModeFlags.Read);
        FileAccess playerFile = FileAccess.Open($"{_pathChoices}{save}/PlayerSettings.json", FileAccess.ModeFlags.Read);
        string gameJson = gameFile.GetAsText();
        string playerJson = playerFile.GetAsText();
        gameFile.Close();
        playerFile.Close();
        Global.Settings.GameSettings = JsonConvert.DeserializeObject<GameSettings>(gameJson);
        Global.Settings.PlayerSettings = JsonConvert.DeserializeObject<PlayerSettings>(playerJson);
        Global.CutSceneData.LoadCutSceneData();
    }

    public void NewGame(string saveName, int saveNumber)
    {
        _directory.CreateSave(saveName, saveNumber);
        SaveGame();
        Global.CutSceneData.LoadCutSceneData();
    }

    public int GetSaveNumber(string save)
    {
        FileAccess file = FileAccess.Open($"{_pathChoices}{save}/GameSettings.json", FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();
        var gameSettings = JsonConvert.DeserializeObject<GameSettings>(json);
        return gameSettings.SaveNumber;
    }

    public List<Save> GetSaves()
    {
        List<string> saves = _directory.GetSaveNames();
        List<Save> result = new List<Save>();
        for (int i = 0; i < saves.Count; i++)
        {
            Save save = new Save();
            save.Name = saves[i];
            save.Number = GetSaveNumber(saves[i]);
            result.Add(save);
        }
        return result;
    }

    public void DeleteGame(string saveName) =>
        _directory.DeleteSave(saveName);

    public void SaveConfig(ConfigInfo config) =>
        _config.SaveConfig(config, _directory);
}
