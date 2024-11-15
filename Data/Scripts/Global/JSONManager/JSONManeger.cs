using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class JSON
{
    private string _pathDialogues = "res://Data/Dialogs/";
    private string _pathChoices = "user://Saves/";

    public JSON()
    {
        Global.Settings.Saves = GetSaves();
        Global.Settings.Saves.Sort();
    }

    public List<NPCDialogue> GetDialogues()
    {
        string path = Global.Settings.GameSettings.CurrentLocation;
        FileAccess file = FileAccess.Open($"{ _pathDialogues}{Global.Settings.GameSettings.CurrentLocation}.json", FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
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
    }

    public void NewGame(string saveName, int saveNumber)
    {
        Global.Directory.CreateSave(saveName, saveNumber);
        SaveGame();
        Global.Dialogue.LoadDialogues();
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
        List<string> saves = Global.Directory.GetSaveNames();
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
}
