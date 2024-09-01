using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class JSONManager
{
    private string _pathDialogues = "res://Data/Dialogs/";
    private string _pathChoices = "user://Saves/";

    public List<NPCDialogue> GetDialogues()
    {
        string path = Global.Settings.CurrentLocation;
        FileAccess file = FileAccess.Open($"{ _pathDialogues}{Global.Settings.CurrentLocation}.json", FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();
        return JsonConvert.DeserializeObject<List<NPCDialogue>>(json);
    }

    public List<PlayerChoice> GetPlayerChoices()
    {
        FileAccess file = FileAccess.Open($"{_pathChoices}{Global.Settings.CurrentSave}/Choices/{Global.Settings.CurrentLocation}.json", FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();
        return JsonConvert.DeserializeObject<List<PlayerChoice>>(json);
    }

    public void SetPlayerChoices(List<PlayerChoice> playerChoices)
    {
        FileAccess file = FileAccess.Open($"{_pathChoices}{Global.Settings.CurrentSave}/Choices/{Global.Settings.CurrentLocation}.json", FileAccess.ModeFlags.Write);
        string json = JsonConvert.SerializeObject(playerChoices, Formatting.Indented);
        file.StoreString(json);
        file.Close();
    }

    public void SaveGame()
    {
        FileAccess file = FileAccess.Open($"{_pathChoices}{Global.Settings.CurrentSave}/GameSettings.json", FileAccess.ModeFlags.Write);
        string json = JsonConvert.SerializeObject(new GameSettings()
        {
            SaveNumber = Global.Settings.SaveNumber,
            CurrentLocation = Global.Settings.CurrentLocation,
            CurrentPosition = Global.SceneObjects.Player?.Position ?? new Vector2(16,16),
            CurrentTargetPosition = Global.SceneObjects.Player?.TargetPosition ?? new Vector2(16, 16)
        }, Formatting.Indented);
        file.StoreString(json);
        file.Close();
    }

    public void LoadGame(string save)
    {
        Global.Settings.CurrentSave = save;
        FileAccess file = FileAccess.Open($"{_pathChoices}{save}/GameSettings.json", FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();
        GameSettings gameSettings = JsonConvert.DeserializeObject<GameSettings>(json);
        Global.Settings.CurrentLocation = gameSettings.CurrentLocation;
        Global.Settings.CurrentTargetPosition = gameSettings.CurrentTargetPosition;
        Global.Settings.CurrentPosition = gameSettings.CurrentPosition;
    }

    public void NewGame()
    {
        Global.Settings.CurrentLocation = "Test/Test";
        SaveGame();
    }

    public int GetSaveNumber(string save)
    {
        FileAccess file = FileAccess.Open($"{_pathChoices}{save}/GameSettings.json", FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();
        var gameSettings = JsonConvert.DeserializeObject<GameSettings>(json);
        return gameSettings.SaveNumber;
    }
}
