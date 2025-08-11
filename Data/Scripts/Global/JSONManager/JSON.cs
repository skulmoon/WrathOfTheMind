using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

public class JSON
{
    private const string PATH_DIALOGUES = "res://Data/Dialogs/";
    private const string PATH_PAMS = "res://Data/PAMS/";
    private const string PATH_SAVES = "user://Saves/";

    private ConfigLoader _config = new ConfigLoader();
    private JsonSerializerSettings _settingsAllSave = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects };

    public ConfigInfo ConfigInfo { get => _config.ConfigInfo; }
     
    private T GetJsonData<T>(string path, bool readAll = false)
    {
        FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        string json = file?.GetAsText() ?? "";
        file?.Close();
        return readAll ? JsonConvert.DeserializeObject<T>(json, _settingsAllSave) : JsonConvert.DeserializeObject<T>(json);
    }

    private void SetJsonData<T>(T data, string path, bool saveAll = false)
    {
        string jsonTask = saveAll ? JsonConvert.SerializeObject(data, Formatting.Indented, _settingsAllSave) : JsonConvert.SerializeObject(data, Formatting.Indented);
        FileAccess file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        file?.StoreString(jsonTask);
        file?.Close();
    }

    public List<NPCPAMS> GetNpcpams() =>
        GetJsonData<List<NPCPAMS>>($"{PATH_PAMS}{Global.Settings.SaveData.CurrentLocation}.json");

    public List<NPCDialogue> GetDialogues() =>
        GetJsonData<List<NPCDialogue>>($"{PATH_DIALOGUES}{Global.Settings.SaveData.CurrentLocation}.json");

    public List<PlayerChoice> GetPlayerChoices() =>
        GetJsonData<List<PlayerChoice>>($"{PATH_SAVES}{Global.Settings.CurrentSave}/Choices/{Global.Settings.SaveData.CurrentLocation}.json");

    public List<(int ID, object Value)> GetLocationData() =>
        GetJsonData<List<(int ID, object Value)>>($"{PATH_SAVES}{Global.Settings?.CurrentSave}/LocationsData/{Global.Settings?.SaveData?.CurrentLocation}.json");

    public SaveData GetSaveData(string save) =>
        GetJsonData<SaveData>($"{PATH_SAVES}{save}/SaveData.json", true);

    public void SetPlayerChoices(List<PlayerChoice> playerChoices) =>
        SetJsonData(playerChoices, $"{PATH_SAVES}{Global.Settings.CurrentSave}/Choices/{Global.Settings.SaveData.CurrentLocation}.json");

    public void SetLocationData(List<(int ID, object Value)> locationData) =>
        SetJsonData(locationData, $"{PATH_SAVES}{Global.Settings.CurrentSave}/LocationsData/{Global.Settings.SaveData.CurrentLocation}.json");

    public void SetSaveData(SaveData settings) =>
        SetJsonData(settings, $"{PATH_SAVES}{Global.Settings.CurrentSave}/SaveData.json", true);

    public void SaveConfig(ConfigInfo config) =>
        _config.SaveConfig(config);
}
