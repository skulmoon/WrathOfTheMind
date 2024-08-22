using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public partial class JSONManager
{
    private List<NPCDialogue> _dialogues;
    private string _pathJson = "res://Data/Dialogs/Test.json";

    public string PathJson
    {
        get
        {
            return _pathJson;
        }
        set
        {
            _pathJson = value;
            SetDialogues();
        }
    }

    public List<NPCDialogue> SetDialogues()
    {
        FileAccess file = FileAccess.Open(_pathJson, FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();
        return JsonConvert.DeserializeObject<List<NPCDialogue>>(json);
    }
}
