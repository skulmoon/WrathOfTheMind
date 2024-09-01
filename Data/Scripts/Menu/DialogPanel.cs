using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

public partial class DialogPanel : NinePatchRect
{
	private DialogText _dialogText;
    private NPCDialogue _dialogue;
    private bool _startDialog = false;
    private int[] _numberOptions = new int[5];
    private int _currentDialog = 0;

    public override void _Ready()
    {
        _dialogText = GetNode<RichTextLabel>("DialogText") as DialogText;
        Global.SceneObjects.DialoguePanel = this;
    }

    public void OutputSpeech(NPCDialogue dialogue)
    {
        if (_dialogue == null)
        {
            _dialogText.ClearText();
            _dialogue = dialogue;
            //_dialogue.DialogueNumber = dialogue.DialogueNumber;
            //_dialogue.Options = dialogue.Options;
            //_dialogue.Speech = dialogue.Speech;
            //_dialogue.NPCID = dialogue.NPCID;
            _dialogText.CurrentPosition = 0;
            _currentDialog = 0;
            Visible = true;
            Global.Settings.CutScene = true;
            _dialogText.Text = $"{_dialogue.Speech[_currentDialog].Name}:\n  {_dialogue.Speech[_currentDialog].Text}";
        }
        else
            _dialogue = null;
    }

    public override void _Input(InputEvent @event)
    {
        if (_dialogText.Control.Visible)
        {
            if (@event.IsActionPressed("up"))
            {
                _dialogText.UpOption();
            }
            else if (@event.IsActionPressed("down"))
            {
                _dialogText.DownOption();
            }
            else if (@event.IsActionPressed("interact"))
            {
                _dialogText.Control.Visible = false;
                _currentDialog = 0;
                int NPCID = _dialogue.NPCID;
                int DialogueNumber = _dialogue.DialogueNumber;
                _dialogue = null;
                Global.DialogueManager.GetDialogue(NPCID, _numberOptions[_dialogText.CurrentPosition], DialogueNumber);
            }
        }
        else if (Visible && @event.IsActionPressed("interact")) 
        {
            if (_currentDialog < _dialogue.Speech.Count-1)
            {
                _currentDialog++;
                _dialogText.Text = $"{_dialogue.Speech[_currentDialog].Name}:\n  {_dialogue.Speech[_currentDialog].Text}";
            }
            else if (_dialogue.Options != null)
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
                Visible = false;
                Global.Settings.CutScene = false;
            }
        }
    }
}
