using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class Dialogue : Node
{
	private List<NPCDialogue> _dialogues;
    private List<PlayerChoice> _playerChoices;

    public void LoadDialogues()
	{
        _dialogues = Global.JSON.GetDialogues() ?? new List<NPCDialogue>();
        _playerChoices = Global.JSON.GetPlayerChoices() ?? new List<PlayerChoice>();
    }

    public void SaveDialogues() =>
        Global.JSON.SetPlayerChoices(_playerChoices);

    public NPCDialogue GetDialogue(int NPCID, int DialogueNumber)
	{
        foreach (NPCDialogue dialogue in _dialogues)
			if (dialogue.NPCID == NPCID && dialogue.DialogueNumber == DialogueNumber)
                return dialogue;
        return null;
    }

    public NPCDialogue GetDialogue(int NPCID, int DialogueNumber, int lastDialogueNumber)
    {
        bool IsFound = false;
        PlayerChoice choice = new PlayerChoice()
        { 
            NPCID = NPCID,
            DialogueNumber = lastDialogueNumber,
            Choice = DialogueNumber
        };

        for (int i = 0; i < _playerChoices.Count; i++)
            if (_playerChoices[i].NPCID == NPCID && _playerChoices[i].DialogueNumber == lastDialogueNumber)
            {
                _playerChoices[i].Choice = choice.Choice;
                IsFound = true;
                break;
            }
        if (!IsFound)
            _playerChoices.Add(choice);
        return GetDialogue(NPCID, DialogueNumber);
    }

    public int GetChoice(int NPCID, int DialogueNumber)
    {
        foreach (PlayerChoice choice in _playerChoices)
            if (choice.NPCID == NPCID && choice.DialogueNumber == DialogueNumber)
                return choice.Choice;
        return -1;
    }
}
