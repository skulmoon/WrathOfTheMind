using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class DialogueManager : Node
{
	private List<NPCDialogue> _dialogues;
    private List<PlayerChoice> _playerChoices;

    public void LoadDialogues()
	{
        _dialogues = Global.JSON.GetDialogues() ?? new List<NPCDialogue>();
        _playerChoices = Global.JSON.GetPlayerChoices() ?? new List<PlayerChoice>();
    }

    public void SaveDialogues()
    {
        GD.Print(_playerChoices.Count);
        Global.JSON.SetPlayerChoices(_playerChoices);
    }

    public void GetDialogue(int NPCID, int DialogueNumber)
	{
        foreach (NPCDialogue dialogue in _dialogues)
		{
			if (dialogue.NPCID == NPCID && dialogue.DialogueNumber == DialogueNumber)
			{
                Global.SceneObjects.DialoguePanel.OutputSpeech(dialogue);
                break;
			}
		}
    }

    public void GetDialogue(int NPCID, int DialogueNumber, int lastDialogueNumber)
    {
        bool IsFound = false;
        PlayerChoice choice = new PlayerChoice()
        { 
            NPCID = NPCID,
            DialogueNumber = lastDialogueNumber,
            Choice = DialogueNumber
        };

        for (int i = 0; i < _playerChoices.Count; i++)
        {
            if (_playerChoices[i].NPCID == NPCID && _playerChoices[i].DialogueNumber == lastDialogueNumber)
            {
                _playerChoices[i].Choice = choice.Choice;
                GD.Print("90");
                IsFound = true;
                break;
            }
        }
        if (!IsFound)
        {
            _playerChoices.Add(choice);
        }
        GetDialogue(NPCID, DialogueNumber);
    }

    public int GetChoice (int NPCID, int DialogueNumber)
    {
        foreach (PlayerChoice choice in _playerChoices)
        {
            if (choice.NPCID == NPCID && choice.DialogueNumber == DialogueNumber)
            {
                return choice.Choice;
            }
        }
        return -1;
    }
}
