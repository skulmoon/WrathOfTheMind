using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.WebSockets;

public partial class DialogPanel : TextureRect
{
    private DialogText _dialogText;
    private NPCDialogue _dialogue;
    private AudioStreamPlayer _player;
    private Timer _delayTimer = new Timer { OneShot = true };
    private bool _startDialog = false;

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
    }

    public override void _Process(double delta)
    {
        if (IsSelected && Global.CutSceneManager.IsChargeComplete && !_dialogText.IsAnimate)
        {
            if (Input.IsActionJustPressed("up"))
                _dialogText.UpOption();
            else if (Input.IsActionJustPressed("down"))
                _dialogText.DownOption();
            else if (Input.IsActionJustPressed("interact"))
            {
                _delayTimer.Start(0.1);
                int option = _dialogText.CurrentPosition;
                _dialogText.EndOptions();
                Global.CutSceneManager.OutputCutScene(_dialogue.Options[option].NextDialogue);
            }
        }
    }

    public void NextDialogue(int currentCutScene)
    {
        if (_dialogue == null)
            return;
        if (_dialogue.Speech.Count > currentCutScene)
        {
            if (_dialogue.Speech[currentCutScene] != null)
            {
                PanelShow();
                _dialogText.PrintText(_dialogue.Speech[currentCutScene].Text, _dialogue.Speech[currentCutScene].Name);
            }
            else
                EndDialogue();
        }
        else
            EndDialogue();
    }
    
    public void EndDialogue()
    {
        _dialogText.ClearText();
        PanelHide();
    }

    public void ShowOptions()
    {
        if (_dialogue.Options != null)
            _dialogText.StartOptions(_dialogue.Options);
    }

    public void PanelShow()
    {
        if (!Visible)
        {
            Visible = true;
            Tween tween = CreateTween();
            tween.TweenProperty(this, "anchor_bottom", 1f, 0.4f).SetTrans(Tween.TransitionType.Sine);
            tween.Parallel().TweenProperty(this, "anchor_top", 0.625f, 0.4f).SetTrans(Tween.TransitionType.Sine);
        }
    }

    public void PanelHide()
    {
        if (Visible)
        {
            Tween tween = CreateTween();
            tween.TweenProperty(this, "anchor_bottom", 1.5f, 0.4f).SetTrans(Tween.TransitionType.Sine);
            tween.Parallel().TweenProperty(this, "anchor_top", 1.125f, 0.4f).SetTrans(Tween.TransitionType.Sine);
            tween.TweenCallback(new Callable(this, nameof(SetVisible)));
        }
    }

    public void EndSpeech() =>
        _dialogText.StopPrinting();

    public void SetVisible() =>
        Visible = false;
}
