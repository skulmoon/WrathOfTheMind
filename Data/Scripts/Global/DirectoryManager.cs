using Godot;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class DirectoryManager
{
	private string _directory;

	public DirectoryManager()
	{
		_directory = ProjectSettings.GlobalizePath("user://Saves");
        Directory.CreateDirectory(_directory);
    }

	public void CreateSave(string saveName, int saveNumber)
	{
		string path = Path.Combine(_directory, saveName);

        if (Directory.Exists(path)) 
		{
			return;
		}
        Directory.CreateDirectory(path);
        Directory.CreateDirectory(Path.Combine(path, "Choices"));
        File.WriteAllText(Path.Combine(path, "GameSettings.json"), "");
        Global.Settings.CurrentSave = saveName;
        Global.Settings.SaveNumber = saveNumber;
        CreateLocationChoices("Test/Test");
        Global.JSONManager.NewGame();
        Global.DialogueManager.LoadDialogues();
    }

    public void DeleteSave(string saveName)
    {
        string path = Path.Combine(_directory, saveName);
        Directory.Delete(path);
    }

    public void CreateLocationChoices(string location)
    {
        string path = Path.Combine(_directory, Global.Settings.CurrentSave, "Choices", location + ".json");
        if (File.Exists(path))
        {
            return;
        }
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, "");
    }

    public List<Save> GetSaveNames()
    {
        var saves = Directory.GetDirectories(_directory).Select(x => Path.GetFileName(x)).ToList();
        List<Save> result = new List<Save>();
        for(int i = 0; i < saves.Count; i++)
        {
            Save save = new Save();
            save.Name = saves[i];
            save.Number = Global.JSONManager.GetSaveNumber(saves[i]);
            result.Add(save);
        }
        return result;
    }
}
