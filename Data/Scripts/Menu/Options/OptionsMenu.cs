using Godot;
using System;
using System.Collections.Generic;

public partial class OptionsMenu : Control
{
	public void SetConfigInfo()
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
		Global.JSON.SaveConfig(config);
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
