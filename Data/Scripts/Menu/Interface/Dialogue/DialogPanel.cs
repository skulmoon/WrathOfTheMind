using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.WebSockets;

public partial class DialogPanel : NinePatchRect
{
    private DialogText _dialogText;
    private NPCDialogue _dialogue;
    private AudioStreamPlayer _player; 
    private Timer _delayTimer = new Timer { OneShot = true };
    private bool _startDialog = false;
    private int[] _numberOptions = new int[5];

    public bool IsSelected { get => (_dialogText?.Control?.Visible ?? false) || _delayTimer.TimeLeft != 0; }
    public bool IsPrinting { get => _dialogText?.IsPrinting ?? false; }

    public DialogPanel() =>
        Global.SceneObjects.DialoguePanel = this;

    public override void _Ready()
    {
        _dialogText = GetNode<DialogText>("DialogText");
        _player = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        AddChild(_delayTimer);
    }

    public void OutputSpeech(NPCDialogue dialogue)
    {
        _dialogue = dialogue;
        PanelShow();
    }

    public override void _Process(double delta)
    {
        if (IsSelected && Global.CutSceneManager.IsChargeComplete)
        {
            if (Input.IsActionJustPressed("up"))
                _dialogText.UpOption();
            else if (Input.IsActionJustPressed("down"))
                _dialogText.DownOption();
            else if (Input.IsActionJustPressed("interact"))
            {
                _delayTimer.Start(0.1);
                _dialogText.Control.Visible = false;
                Global.CutSceneManager.OutputCutScene(_numberOptions[_dialogText.CurrentPosition]);
            }
        }
    }

    public void NextDialogue(int currentCutScene)
    {
        _dialogText.PrintText(_dialogue.Speech[currentCutScene].Text, _dialogue.Speech[currentCutScene].Name);
    }

    public void EndDialogue()
    {
        _dialogText.ClearText();
        PanelHide();
    }

    public void ShowOptions()
    {
        if (_dialogue.Options != null)
        {
            _dialogText.Text = "";
            _dialogText.Control.Visible = true;
            _dialogText.CountOfOptions = _dialogue.Options.Count;
            for (int i = 0; i < _dialogText.CountOfOptions; i++)
            {
                _dialogText.OptionsText[i].Text = _dialogue.Options[i].OptionText;
                _numberOptions[i] = _dialogue.Options[i].NextDialogue;
            }
        }
    }

    public void PanelShow() =>
        Visible = true;

    public void PanelHide() =>
        Visible = false;

    public void EndSpeech()
    {
        GD.Print(10);
        _dialogText.StopPrinting();
    }
}
