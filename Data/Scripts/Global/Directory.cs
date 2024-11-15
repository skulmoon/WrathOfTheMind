using Godot;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class Directory
{
	private string _directory;

	public Directory()
	{
		_directory = ProjectSettings.GlobalizePath("user://Saves");
        System.IO.Directory.CreateDirectory(_directory);
    }

	public void CreateSave(string saveName, int saveNumber)
	{
		string path = Path.Combine(_directory, saveName);

        if (System.IO.Directory.Exists(path)) 
		{
			return;
		}
        System.IO.Directory.CreateDirectory(path);
        System.IO.Directory.CreateDirectory(Path.Combine(path, "Choices"));
        File.WriteAllText(Path.Combine(path, "GameSettings.json"), "");
        File.WriteAllText(Path.Combine(path, "PlayerSettings.json"), "");
        Global.Settings.GameSettings = new GameSettings()
        {
            SaveNumber = saveNumber,
            CurrentLocation = "Test/Test"
        };
        Global.Settings.PlayerSettings = new PlayerSettings()
        {
            Items = new List<Item>(),
            Weapons = new List<Item>(),
            Scruples = 0,
            CurrentTargetPosition = new Vector2(16, 16),
            CurrentPosition = new Vector2(16, 16),
        };
        Global.Settings.CurrentSave = saveName;
        Global.Settings.GameSettings.SaveNumber = saveNumber;
        CreateLocationChoices("Test/Test");
    }

    public void DeleteSave(string saveName)
    {
        string path = Path.Combine(_directory, saveName);
        System.IO.Directory.Delete(path);
    }

    public void CreateLocationChoices(string location)
    {
        string path = Path.Combine(_directory, Global.Settings.CurrentSave, "Choices", location + ".json");
        if (File.Exists(path))
        {
            return;
        }
        System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, "");
    }

    public List<string> GetSaveNames()
    {
        var result = System.IO.Directory.GetDirectories(_directory).Select(x => Path.GetFileName(x)).ToList();
        return result;
    }
}
