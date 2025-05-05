using Godot;
using System;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class CutSceneManager : Node
{
    private NPCPAMS _npcPams;
    private NPCDialogue _dialogue;
    private AudioStreamPlayer _player;
    private DialogPanel _panel;
    private int _currentCutScene = 0;

    public CutSceneManager()
    {
        Global.SceneObjects.OnDialoguePanelChanged += TakePanel;
        Global.SceneObjects.OnStorageReady += (storage) => storage.GetTree().Root.CallDeferred("add_child", this);
    }

    private void TakePanel(Node node)
    {
        _panel = (DialogPanel)node;
        _player = new AudioStreamPlayer();
        _panel.AddChild(_player);
    }

    public void OutputCutScene(int NPCID, int dialogueNumber)
    {
        if (_dialogue != null)
        {
            _dialogue = null;
            return;
        }
        Global.CutSceneData.GetCutSceneData(out _npcPams, out NPCDialogue npsDialogue, NPCID, dialogueNumber);
        StartCutScene(npsDialogue);
    }

    public void OutputCutScene(int NextDialogueNumber)
    {
        Global.CutSceneData.GetCutSceneData(out _npcPams, out NPCDialogue npsDialogue, _dialogue.NPCID, NextDialogueNumber, _dialogue.DialogueNumber);
        StartCutScene(npsDialogue);
    }

    public void OutputCutScene(List<PAMS> pams)
    {
        _npcPams = new NPCPAMS { PAMSs = pams };
        StartCutScene(null);
    }

    public override void _Input(InputEvent @event)
    {
        if (Global.Settings.CutScene && @event.IsActionPressed("interact") && !(Global.SceneObjects.InventoryMenu?.Visible ?? false))
        {
            if (_currentCutScene < _npcPams.PAMSs.Count - 1)
            {
                _currentCutScene++;
                if (_dialogue != null)
                    _panel.NextDialogue(_currentCutScene);
                NextPAMS(_currentCutScene);
            }
            else
            {
                _currentCutScene = 0;
                if (_dialogue != null)
                    _panel.EndDialogue();
                else
                    Global.Settings.CutScene = false;
            }
        }
    }

    public void StartCutScene(NPCDialogue npcDialogue)
    {
        Global.Settings.CutScene = true;
        _dialogue = npcDialogue;
        if (npcDialogue != null)
        {
            _panel.OutputSpeech(npcDialogue);
            _panel.NextDialogue(_currentCutScene);
        }
        NextPAMS(_currentCutScene);
    }

    public void NextPAMS(int currentCutScene)
    {
        if (_npcPams?.PAMSs[currentCutScene] != null)
        {
            if (_npcPams.PAMSs[currentCutScene].Music != null)
                Global.Music.PlayMusic(_npcPams.PAMSs[currentCutScene].Music);
            if (_npcPams.PAMSs[currentCutScene].Sound != null)
                Global.Music.PlaySound(_npcPams.PAMSs[currentCutScene].Sound);
            if (_npcPams.PAMSs[currentCutScene].PAData != null)
                foreach (PAData paData in _npcPams.PAMSs[currentCutScene].PAData)
                    foreach (NPC npc in Global.SceneObjects.Npcs)
                        if (paData.NPCID == npc.ID)
                            if (paData?.Data != null)
                                npc.GetPA(paData);
        }
    }
}
