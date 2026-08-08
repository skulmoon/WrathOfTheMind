using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public partial class ConfigManager : Node
{
    private bool _isNew;
    private JsonSerializerSettings _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    private Directory _directory = new Directory();

    public ConfigInfo ConfigInfo { get; set; }

    public ConfigManager() 
	{
        ConfigInfo = GetConfig();
        if (!_isNew)
            LoadConfig();
        else
            TranslationServer.SetLocale("ru");
    }

    public void SaveConfig()
    {
        List<(string, List<(long?, int?)>)> keyActionList = new List<(string, List<(long?, int?)>)>();
        foreach (var action in GetCustomActions())
        {
            List<(long?, int?)> keys = new List<(long?, int?)>();
            foreach (var key in InputMap.ActionGetEvents(action))
                if (key is InputEventKey eventKey)
                    keys.Add(((long)eventKey.Keycode, null));
                else if (key is InputEventMouseButton eventMouse)
                    keys.Add((null, (int)eventMouse.ButtonIndex));
            keyActionList.Add((action, keys));
        }
        ConfigInfo config = new ConfigInfo
        {
            Base = new ConfigBase
            {
                Language = TranslationServer.GetLocale() ?? "ru",
                FistCutSceneActivated = Global.Variables.FistCutSceneActivated,
            },
            Graphics = new ConfigGraphics
            {

            },
            Sound = new ConfigSound
            {
                Base = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master")),
                Environment = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Sound")),
                Music = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Music")),
            },
            Control = new ConfigControl
            {
                KeyActionList = keyActionList,
            }
        };
        string configJson = JsonConvert.SerializeObject(config, Formatting.Indented, _settings);
        _directory.SaveConfig(configJson);
    }

    public ConfigInfo GetConfig()
    {
        FileAccess file = FileAccess.Open($"user://Config.json", FileAccess.ModeFlags.Read);
        _isNew = file == null;
        string json = file?.GetAsText() ?? "";
        file?.Close();
        return JsonConvert.DeserializeObject<ConfigInfo>(json);
    }

    public void LoadConfig()
    {
        TranslationServer.SetLocale(ConfigInfo.Base.Language); //Base
        Global.Variables.FistCutSceneActivated = ConfigInfo.Base.FistCutSceneActivated;
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), ConfigInfo.Sound.Base); //Sound
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Sound"), ConfigInfo.Sound.Environment);
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), ConfigInfo.Sound.Music);
        foreach (var action in ConfigInfo.Control.KeyActionList) //Control
        {
            InputMap.ActionEraseEvents(action.Item1);
            foreach (var item in action.Item2 ?? new List<(long?, int?)>())
                if (item.Item1 != null)
                    InputMap.ActionAddEvent(action.Item1, new InputEventKey { Keycode = (Key)(item.Item1 ?? 0) });
                else if (item.Item2 != null)
                    InputMap.ActionAddEvent(action.Item1, new InputEventMouseButton { ButtonIndex = (MouseButton)(item.Item2 ?? 0) });
        }
    }

    public static List<string> GetCustomActions()
    {
        List<string> result = new List<string>();
        foreach (var action in InputMap.GetActions())
            if (!((string)action).StartsWith("ui_"))
                result.Add(action);
        return result;
    }
}
