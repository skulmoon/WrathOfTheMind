using Godot;
using System;
using System.Collections.Generic;

public partial class CutSceneData
{
    private List<NPCPAMS> _npsPamses = new List<NPCPAMS>();
    private Dialogue _dialogue = new Dialogue();

    public CutSceneData()
    {
        Global.SceneObjects.LocationChanged += OnLocationLoad;
    }
    
    public void OnLocationLoad(Node node)
    {
        if (node is Location location)
        {
            string[] x = location.SceneFilePath.Split('/', '.')[^3..^1];
            Global.Settings.GameSettings.CurrentLocation = $"{x[0]}\\{x[1]}";
            Global.CutSceneData.LoadCutSceneData();
        }
    }

    public void LoadCutSceneData()
    {
        _npsPamses = Global.JSON.GetNpcpams();
        _dialogue.LoadDialogues();
    }

    public void GetCutSceneData(out NPCPAMS npsPamses, out NPCDialogue npsDialogues, int NPCID, int DialogueNumber)
    {
        npsDialogues = _dialogue.GetDialogue(NPCID, DialogueNumber);
        npsPamses = GetNPCPAMS(NPCID, DialogueNumber);
    }

    public void GetCutSceneData(out NPCPAMS npsPamses, out NPCDialogue npsDialogues, int NPCID, int DialogueNumber, int lastDialogueNumber)
    {
        npsDialogues = _dialogue.GetDialogue(NPCID, DialogueNumber, lastDialogueNumber);
        npsPamses = GetNPCPAMS(NPCID, DialogueNumber);
    }

    public NPCPAMS GetNPCPAMS(int NPCID, int DialogueNumber)
    {
        if(_npsPamses != null)
            foreach (NPCPAMS buffer in _npsPamses)
                if (buffer.NPCID == NPCID && buffer.DialogueNumber == DialogueNumber)
                    return buffer;
        return null;
    }

    public int GetChoice(int NPCID, int DialogueNumber) =>
        _dialogue.GetChoice(NPCID, DialogueNumber);

    public void SaveChoices() =>
        _dialogue.SaveDialogues();
}
