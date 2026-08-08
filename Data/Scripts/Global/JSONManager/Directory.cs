using Godot;
using System.Collections.Generic;
using System.IO;
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
        File.WriteAllText(Path.Combine(path, "SaveData.json"), "");
        SaveData saveData = new SaveData()
        {
            SaveNumber = saveNumber,
            CurrentLocation = "Prologue/TheBeginning",
            Items = new List<Item>(new Item[33]),
            Shards = new List<Item>(new Item[21]),
            Armors = new List<Item>(new Item[21]),
            Scruples = 0,
            CurrentPosition = new Vector2(160, 400),
        };
        for (int i = 0; i < 24; i++)
            saveData.Items[i] = Global.ItemFabric.CreateItem(0, 12);
        Global.Variables.SaveData = saveData;
        Global.Variables.CurrentSave = saveName;
        Global.Variables.Saves.Add(new Save() { Name = saveName, Number = saveNumber });
        Global.Variables.Saves.Sort();
        CreateLocationData("Prologue/TheBeginning");
    }

    public void DeleteSave(string saveName)
    {
        string path = Path.Combine(_directory, saveName);
        System.IO.Directory.Delete(path, true);
        Global.Variables.Saves.Remove(Global.Variables.Saves.Find((x) => x.Name == saveName));
        Global.Inventory.Clear();
    }

    public void CreateLocationData(string location)
    {
        string path = Path.Combine(_directory, Global.Variables.CurrentSave, "Choices", location + ".json");
        if (File.Exists(path))
        {
            return;
        }
        System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, "");
        path = Path.Combine(_directory, Global.Variables.CurrentSave, "LocationsData", location + ".json");
        if (File.Exists(path))
        {
            return;
        }
        System.IO.Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, "null");
    }

    public List<string> GetSaveNames()
    {
        var result = System.IO.Directory.GetDirectories(_directory).Select(x => Path.GetFileName(x)).ToList();
        return result;
    }

    public void SaveConfig(string config) =>
        File.WriteAllText(Path.Combine(ProjectSettings.GlobalizePath("user://"), "Config.json"), config);
}
