using Godot;
using System.IO;
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
        GameSettings gameSettings = new GameSettings()
        {
            SaveNumber = saveNumber,
            CurrentLocation = "Test/Test"
        };
        Global.Settings.GameSettings = gameSettings;
        PlayerSettings playerSettings = new PlayerSettings()
        {
            Items = new List<Item>(new Item[33]),
            Shards = new List<Item>(new Item[21]),
            Armors = new List<Item>(new Item[21]),
            Scruples = 0,
            CurrentTargetPosition = new Vector2(16, 16),
            CurrentPosition = new Vector2(16, 16),
        };
        for (int i = 0; i < 24; i++)
            playerSettings.Items[i] = Global.ItemFabric.CreateItem(0, 12);
        Global.Settings.PlayerSettings = playerSettings;
        Global.Settings.CurrentSave = saveName;
        Global.Settings.GameSettings.SaveNumber = saveNumber;
        Global.Settings.Saves.Add(new Save() { Name = saveName, Number = saveNumber });
        Global.Settings.Saves.Sort();
        CreateLocationChoices("Test/Test");
    }

    public void DeleteSave(string saveName)
    {
        string path = Path.Combine(_directory, saveName);
        System.IO.Directory.Delete(path, true);
        Global.Settings.Saves.Remove(Global.Settings.Saves.Find((x) => x.Name == saveName));
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
