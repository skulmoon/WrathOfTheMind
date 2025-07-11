using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public partial class ConfigLoader : Node
{
    private bool _isNew;
    private JsonSerializerSettings _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
    private Directory _directory = new Directory();

    public ConfigInfo ConfigInfo { get; set; }

    public ConfigLoader() 
	{
        ConfigInfo = GetConfig();
        if (!_isNew)
        {
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
    }

    public void SaveConfig(ConfigInfo config)
    {
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
}
