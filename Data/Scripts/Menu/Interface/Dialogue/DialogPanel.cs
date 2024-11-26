using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Net.WebSockets;

public partial class DialogPanel : NinePatchRect
{
    private DialogText _dialogText;
    private NPCDialogue _dialogue;
    private AudioStreamPlayer _player;
    private bool _startDialog = false;
    private int[] _numberOptions = new int[5];

    public bool IsSelected { get => _dialogText.Control.Visible; }
    public bool IsPrinting { get => _dialogText.IsPrinting; }

    public override void _Ready()
    {
        Global.SceneObjects.DialoguePanel = this;
        _dialogText = GetNode<DialogText>("DialogText");
        _player = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    public void OutputSpeech(NPCDialogue dialogue)
    {
        _dialogue = dialogue;
        PanelShow();
    }

    public override void _Input(InputEvent @event)
    {
        if (_dialogText.Control.Visible)
        {
            if (@event.IsActionPressed("up"))
                _dialogText.UpOption();
            else if (@event.IsActionPressed("down"))
                _dialogText.DownOption();
            else if (@event.IsActionPressed("interact"))
            {
                _dialogText.Control.Visible = false;
                Global.CutSceneManager.OutputCutScene(_numberOptions[_dialogText.CurrentPosition]);
            }
        }
        else if (Visible && @event.IsActionPressed("interact") && _dialogText.IsPrinting)
            _dialogText.StopPrinting();
        else if (Global.Settings.CutScene && @event.IsActionPressed("interact") && !IsPrinting)
            Global.CutSceneManager._Input(@event);
    }

    public void NextDialogue(int currentCutScene) =>
        _dialogText.PrintText(_dialogue.Speech[currentCutScene].Text, _dialogue.Speech[currentCutScene].Name);

    public void EndDialogue()
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
        else
        {
            _dialogText.ClearText();
            Global.Settings.CutScene = false;
            PanelHide();
        }
    }

    public void PanelShow() =>
        Visible = true;

    public void PanelHide() =>
        Visible = false;
}
