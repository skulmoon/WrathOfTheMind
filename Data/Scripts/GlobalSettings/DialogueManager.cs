using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class DialogueManager : Node
{
	private List<NPCDialogue> dialogues;
	private RichTextLabel dialogText;
    private string _pathJson = "res://Data/Dialogs/Test.json";

    public DialogPanel DialoguePanel { get; set; }
    public string PathJson { 
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

	public DialogueManager()
	{
        SetDialogues();
    }

    public override void _Ready()
    {
         DialoguePanel = GetNode<DialogPanel>("/root/Test/Interface/DialogPanel");
    }

    private void SetDialogues()
	{
        FileAccess file = FileAccess.Open(_pathJson, FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();
		dialogues = JsonConvert.DeserializeObject<List<NPCDialogue>>(json);
    }

	public void GetDialogue(int NPCID, int DialogueNumber)
	{
        foreach (NPCDialogue dialogue in dialogues)
		{
			if (dialogue.NPCID == NPCID && dialogue.DialogueNumber == DialogueNumber)
			{
	
                DialoguePanel.OutputSpeech(dialogue.Speech, dialogue.Options, NPCID);
                break;
			}
		}
    }
}

public class NPCDialogue
{
	public int NPCID { get; set; }
	public int DialogueNumber { get; set; }
	public List<IDAndText> Speech { get; set; }
	public List<Options> Options { get; set; }
}

public class IDAndText
{
    public string Name { get; set; }
    public string Text { get; set; }
	public string Image { get; set; }
}

public class Options
{
    public int NextDialogue { get; set; }
	public string OptionText { get; set;}
}