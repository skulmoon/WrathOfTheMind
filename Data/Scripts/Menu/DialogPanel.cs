using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

public partial class DialogPanel : NinePatchRect
{
	private DialogText _dialogText;
    private List<IDAndText> _speech;
    private List<Options> _options;
    private bool _startDialog = false;
    private int[] _numberOptions = new int[5];
    private int _currentDialog = 0;
    private int _npcID;

    public override void _Ready()
    {
        _dialogText = GetNode<RichTextLabel>("DialogText") as DialogText;
        Global.DialogueManager.DialoguePanel = this;
    }

    public void OutputSpeech(List<IDAndText> speech, List<Options> options, int npcID)
    {
        if (_speech == null)
        {
            _dialogText.ClearText();
            _speech = speech;
            _options = options;
            _npcID = npcID;
            _dialogText.CurrentPosition = 0;
            _currentDialog = 0;
            Visible = true;
            Global.Settings.CutScene = true;
            _dialogText.Text = $"{_speech[_currentDialog].Name}:\n  {_speech[_currentDialog].Text}";
        }
        else
            _speech = null;
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
                _speech = null;
                Global.DialogueManager.GetDialogue(_npcID, _numberOptions[_dialogText.CurrentPosition]);
            }
        }
        else if (Visible && @event.IsActionPressed("interact")) 
        {
            if (_currentDialog < _speech.Count-1)
            {
                _currentDialog++;
                _dialogText.Text = $"{_speech[_currentDialog].Name}:\n  {_speech[_currentDialog].Text}";
            }
            else if (_options != null)
            {
                _dialogText.Text = "";
                _dialogText.Control.Visible = true;
                _dialogText.CountOfOptions = _options.Count;

                for (int i = 0; i < _dialogText.CountOfOptions; i++)
                {
                    _dialogText.OptionsText[i].Text = _options[i].OptionText;
                    _numberOptions[i] = _options[i].NextDialogue;
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
