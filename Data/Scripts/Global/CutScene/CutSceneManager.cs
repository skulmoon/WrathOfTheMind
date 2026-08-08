using Godot;
using System;
using Godot.Collections;
public partial class CutSceneManager : Node
{
    private Timer _chargeTimer = new Timer { OneShot = true };
    private NPCPAMS _npcPams;
    private NPCDialogue _dialogue;
    private DialogPanel _panel;
    private PAMSController _PAMSController;
    private int _CutSceneCount;
    private int _currentCutScene = 0;

    private Action _startedCutScene;

    public bool IsChargeComplete { get => _chargeTimer.TimeLeft == 0; }
    public bool IsPanelActive { get => _panel.Visible; }

    public event Action StartedCutScene { add { _startedCutScene += value; if (Global.Variables.CutScene) value.Invoke(); } remove => _startedCutScene -= value; }
    public event Action EndedCutScene;

    public CutSceneManager()
    {
        _PAMSController = new PAMSController(this);
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
        if (Global.Variables.CutScene && Input.IsActionJustPressed("interact") && !(Global.SceneObjects.InventoryMenu?.Visible ?? false) && !(_panel?.IsSelected ?? false))
        {
            bool isNext = true;
            if (!(_PAMSController?.IsDone ?? true))
            {
                int lastNumber = _currentCutScene;
                _PAMSController?.EndPAMS();
                if (_currentCutScene != lastNumber)
                    isNext = false;
            }
            if (_panel?.IsPrinting ?? false)
            {
                _panel?.EndSpeech();
                isNext = false;
            }
            if (isNext)
            {
                NextCutScenePart();
            }
        }
    }

    public void NextCutScenePart()
    {
        if (_currentCutScene < _CutSceneCount)
        {
            bool isNotEmpty = false;
            while (!isNotEmpty && _currentCutScene < _CutSceneCount)
            {
                isNotEmpty = true;
                _PAMSController.NextPAMS();
                _panel.NextDialogue(_currentCutScene);
                if (_PAMSController.IsDone && !_panel.IsPrinting)
                    isNotEmpty = false;
                _currentCutScene++;
            }
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
                Global.Variables.CutScene = false;
                EndedCutScene?.Invoke();
            }
        }
    }

    public void StartCutScene(NPCDialogue npcDialogue, Array<PAMS> pamses)
    {
        _startedCutScene?.Invoke();
        Global.Variables.CutScene = true;
        _dialogue = npcDialogue;
        _CutSceneCount = (_npcPams?.PAMSs?.Count ?? 0) > (_dialogue?.Speech?.Count ?? 0) ? (_npcPams?.PAMSs?.Count ?? 0) : (_dialogue?.Speech?.Count ?? 0);
        if (npcDialogue != null)
            _panel.OutputSpeech(npcDialogue);
        if (pamses != null)
            _PAMSController.SetPAMS(pamses);
        NextCutScenePart();
    }

    private void ActiveCharge() =>
        _chargeTimer.Start(0.1f);

    public void Disable()
    {
        _CutSceneCount = 0;
        _currentCutScene = 0;
    }
}
