using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public partial class DialogueManager : Node
{
	private List<NPCDialogue> dialogues;
	public string PathJson { get; set; } = "res://Data/Dialogs/Test.json";

	public DialogueManager()
	{
        FileAccess file = FileAccess.Open(PathJson, FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        file.Close();
        dialogues = JsonSerializer.Deserialize<List<NPCDialogue>>(json);
    }

	public List<string> GetDialogue(int NPCID, int DialogueNumber)
	{
		List<string> returnSpeech = null;

		foreach (NPCDialogue dialogue in dialogues)
		{
			if (dialogue.NPCID == NPCID && dialogue.DialogueNumber == DialogueNumber)
			{
				returnSpeech = dialogue.Speech;
			}
		}

		return returnSpeech;
	}
}

public class NPCDialogue
{
	public int NPCID { get; set; }
	public int DialogueNumber { get; set; }
	public List<string> Speech { get; set; }
}
