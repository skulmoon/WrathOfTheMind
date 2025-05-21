using Godot;
using System;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class CutSceneManager : Node
{
    private Timer _chargeTimer = new Timer { OneShot = true };
    private NPCPAMS _npcPams;
    private NPCDialogue _dialogue;
    private DialogPanel _panel;
    private PAMSController _PAMSController = new PAMSController();
    private int _CutSceneCount;
    private int _currentCutScene = 0;

    public bool IsChargeComplete { get => _chargeTimer.TimeLeft == 0; }
    public bool IsPanelActive { get => _panel.Visible; }

    public event Action EndCutScene;

    public CutSceneManager()
    {
        Global.SceneObjects.DialoguePanelChanged += TakePanel;
        Global.SceneObjects.StorageReady += (storage) => storage.GetTree().Root.CallDeferred("add_child", this);
        AddChild(_chargeTimer);
    }

    private void TakePanel(Node node)
    {
        _panel = (DialogPanel)node;
    }

    public void OutputCutScene(int NPCID, int dialogueNumber)
    {
        Global.CutSceneData.GetCutSceneData(out _npcPams, out _dialogue, NPCID, dialogueNumber);
        StartCutScene(_dialogue, _npcPams?.PAMSs);
    }

    public void OutputCutScene(int NextDialogueNumber)
    {
        var dialogue = _dialogue;
        Global.CutSceneData.GetCutSceneData(out _npcPams, out _dialogue, dialogue.NPCID, NextDialogueNumber, dialogue.DialogueNumber);
        StartCutScene(_dialogue, _npcPams?.PAMSs);
    }

    public override void _Process(double delta)
    {
        if (Global.Settings.CutScene && Input.IsActionJustPressed("interact") && !(Global.SceneObjects.InventoryMenu?.Visible ?? false) && !(_panel?.IsSelected ?? false))
        {
            if (!(_panel?.IsPrinting ?? false))
            {
                if (_currentCutScene < _CutSceneCount - 1)
                {
                    _currentCutScene++;
                    if (_dialogue != null)
                        _panel.NextDialogue(_currentCutScene);
                    _PAMSController.NextPAMS();
                }
                else
                {
                    _currentCutScene = 0;
                    ActiveCharge();
                    if (_dialogue?.Options != null)
                        _panel.ShowOptions();
                    else
                    {
                        _currentCutScene = 0;
                        if (_dialogue != null)
                            _panel.EndDialogue();
                        Global.Settings.CutScene = false;
                    }
                }
            }
            else
                _panel?.EndSpeech();
            _PAMSController?.EndPAMS();
        }
    }

    public void StartCutScene(NPCDialogue npcDialogue, List<PAMS> pamses)
    {
        Global.Settings.CutScene = true;
        _dialogue = npcDialogue;
        _CutSceneCount = _npcPams?.PAMSs?.Count ?? (_dialogue?.Speech?.Count ?? 0);
        if (npcDialogue != null)
        {
            _panel.OutputSpeech(npcDialogue);
            _panel.NextDialogue(_currentCutScene);
        }
        if (pamses != null)
        {
            _PAMSController.SetPAMS(pamses);
            _PAMSController.NextPAMS();
        }
    }

    private void ActiveCharge() =>
        _chargeTimer.Start(0.1f);
}
