using Godot;
using System;
using System.Collections.Generic;

public partial class DialogText : RichTextLabel
{
    private string _text;
    private string _name;
    private int _counter = 0;
    private double _delta = 0.0;

    public Control Control { get; set; }
    public int CurrentPosition { get; set; } = 0;
    public bool IsPrinting { get; private set; } = false;
    public bool IsAnimate { get; private set; } = false; 
    public List<Option> Options { get; private set; }
    [Export] public CustomLabel[] OptionsText { get; set; }
    [Export] public double PrintingSpeed { get; set; } = 0.02;
    [Export] public TextureRect CharapterName { get; set; }
    [Export] public float Duration { get; set; } = 0.5f;

    public override void _Ready()
	{
        Control = GetNode<Control>("%Options");
    }

    public override void _Process(double delta)
    {
        if (IsPrinting)
        {
            _delta += delta;
            while (_delta > PrintingSpeed)
            {
                if (_text[_counter] == '+')
                    _delta -= (PrintingSpeed * 10);
                else
                {
                    Text += _text[_counter];
                    _delta -= PrintingSpeed;
                }
                _counter++;
                if (_counter == _text.Length)
                {
                    IsPrinting = false;
                    _counter = 0;
                }
            }
        }
    }

    public void StartOptions(List<Option> options)
    {
        Text = "";
        Control.Visible = true;
        foreach (var option in OptionsText)
            option.Text = string.Empty;
        Options = options;
        CurrentPosition = 0;
        for (int i = 0; i < options.Count && i < 3; i++)
            OptionsText[i+2].Text = options[i].OptionText;
        GetNode<DialogueButton>("%DialogButtonUp").Visible = true;
        GetNode<DialogueButton>("%DialogButtonDown").Visible = true;
    }

    public void EndOptions()
    {
        Control.Visible = false;
        GetNode<DialogueButton>("%DialogButtonUp").Visible = false;
        GetNode<DialogueButton>("%DialogButtonDown").Visible = false;
    }


    public void UpOption()
	{
        if (CurrentPosition != 0)
        {
            IsAnimate = true;
            CurrentPosition--;
            Tween tween = CreateTween();
            (float, float, Vector2, Color) startParams = (OptionsText[0].AnchorBottom, OptionsText[0].AnchorTop, OptionsText[0].Scale, OptionsText[0].Modulate);
            for (int i = 0; i < OptionsText.Length - 1; i++)
            {
                tween.Parallel().TweenProperty(OptionsText[i], "anchor_top", OptionsText[i + 1].AnchorTop, Duration);
                tween.Parallel().TweenProperty(OptionsText[i], "anchor_bottom", OptionsText[i + 1].AnchorBottom, Duration);
                tween.Parallel().TweenProperty(OptionsText[i], "scale", OptionsText[i + 1].Scale, Duration);
                tween.Parallel().TweenProperty(OptionsText[i], "modulate:a", OptionsText[i + 1].Modulate.A, Duration);
            }
            tween.TweenCallback(Callable.From(() =>
            {
                for (int i = OptionsText.Length - 1; i > 0; i--)
                {
                    OptionsText[i].AnchorTop = OptionsText[i - 1].AnchorTop;
                    OptionsText[i].AnchorBottom = OptionsText[i - 1].AnchorBottom;
                    OptionsText[i].Scale = OptionsText[i - 1].Scale;
                    OptionsText[i].Modulate = OptionsText[i - 1].Modulate;
                }
                (OptionsText[0].AnchorBottom, OptionsText[0].AnchorTop, OptionsText[0].Scale, OptionsText[0].Modulate) = startParams;
                for (int i = 0; i < OptionsText.Length; i++)
                {
                    try
                    {
                        OptionsText[i].Text = Options[i + (CurrentPosition - 2)].OptionText;
                    }
                    catch
                    {
                        OptionsText[i].Text = string.Empty;
                    }
                }
                IsAnimate = false;
            }));

        }
    }

	public void DownOption()
	{
        if (CurrentPosition != Options.Count - 1)
        {
            IsAnimate = true;
            CurrentPosition++;
            Tween tween = CreateTween();
            (float, float, Vector2, Color) startParams = (OptionsText[4].AnchorBottom, OptionsText[4].AnchorTop, OptionsText[4].Scale, OptionsText[4].Modulate);
            for (int i = 1; i < OptionsText.Length; i++)
            {
                tween.Parallel().TweenProperty(OptionsText[i], "anchor_top", OptionsText[i - 1].AnchorTop, Duration);
                tween.Parallel().TweenProperty(OptionsText[i], "anchor_bottom", OptionsText[i - 1].AnchorBottom, Duration);
                tween.Parallel().TweenProperty(OptionsText[i], "scale", OptionsText[i - 1].Scale, Duration);
                tween.Parallel().TweenProperty(OptionsText[i], "modulate:a", OptionsText[i - 1].Modulate.A, Duration);
            }
            tween.TweenCallback(Callable.From(() =>
            {
                for (int i = 0; i < OptionsText.Length - 1; i++)
                {
                    OptionsText[i].AnchorTop = OptionsText[i + 1].AnchorTop;
                    OptionsText[i].AnchorBottom = OptionsText[i + 1].AnchorBottom;
                    OptionsText[i].Scale = OptionsText[i + 1].Scale;
                    OptionsText[i].Modulate = OptionsText[i + 1].Modulate;
                }
                (OptionsText[4].AnchorBottom, OptionsText[4].AnchorTop, OptionsText[4].Scale, OptionsText[4].Modulate) = startParams;
                for (int i = 0; i < OptionsText.Length; i++)
                {
                    try
                    {
                        OptionsText[i].Text = Options[i + (CurrentPosition - 2)].OptionText;
                    }
                    catch
                    {
                        OptionsText[i].Text = string.Empty;
                    }
                }
                IsAnimate = false;
            }));
        }
    }

    public void ChangeOption(int option)
    {
        CurrentPosition = option;
    }

    public void ClearText()
    {
        for (int i = 0; i < 5; i++)
        {
            OptionsText[i].Text = "";
        }
        IsPrinting = false;
        CurrentPosition = 0;
    }

    public void PrintText(string text, string name)
    {
        SetCharacterName(name);
        Text = string.Empty;
        _name = name;
        _text = text;
        IsPrinting = true;
    }

    public void StopPrinting()
    {
        SetCharacterName(_name);
        Text = $"{_text.Replace("+", "")}";
        _counter = 0;
        IsPrinting = false;
    }

    public void SetCharacterName(string name)
    {
        if (name != string.Empty && name != null)
            CharapterName.Visible = true;
        else
            CharapterName.Visible = false;
        CharapterName.GetNode<Label>("Name").Text = name;
    }
}
 