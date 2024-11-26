using Godot;
using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

public partial class DialogText : RichTextLabel
{
    const int MAX_OPTIONS = 5;
	private Panel[] _panels = new Panel[MAX_OPTIONS];
    private string _text;
    private string _name;
    private int _counter = 0;
    private double _delta = 0.0;

    public Control Control { get; set; }
    public Label[] OptionsText { get; set; } = new Label[MAX_OPTIONS];
    public int CountOfOptions { get; set; } 
    public int CurrentPosition { get; set; } = 0;
    public bool IsPrinting { get; private set; } = false;
    [Export] public double PrintingSpeed { get; set; } = 0.02;

    public override void _Ready()
	{
        Control = GetNode<Control>("Options");
        try
        {
            for (int i = 1; i <= MAX_OPTIONS; i++)
            {
                OptionsText[i-1] = GetNode<Label>("Options/OptionText" + i);
                _panels[i-1] = GetNode<Panel>("Options/Option" + i);
            }
        }
        catch { }
    }

    public override void _Process(double delta)
    {
        if (IsPrinting)
        {
            _delta += delta;
            while (_delta > PrintingSpeed)
            {
                Text += _text[_counter];
                _counter++;
                if (_counter == _text.Length)
                {
                    IsPrinting = false;
                    _counter = 0;
                }
                _delta -= PrintingSpeed;
            }
        }
    }

	public void UpOption()
	{
        _panels[CurrentPosition].Visible = false;

        if (CurrentPosition == 0)
            CurrentPosition = CountOfOptions - 1;
        else
            CurrentPosition--;
        _panels[CurrentPosition].Visible = true;
    }

	public void DownOption()
	{
        _panels[CurrentPosition].Visible = false;

        if (CurrentPosition == CountOfOptions - 1)
            CurrentPosition = 0;
        else
            CurrentPosition++;
        _panels[CurrentPosition].Visible = true;
    }

    public void ClearText()
    {
        for (int i = 0;i < MAX_OPTIONS; i++)
        {
            _panels[i].Visible = false;
            OptionsText[i].Text = "";
        }
        _panels[0].Visible = true;
        CurrentPosition = 0;
    }

    public void PrintText(string text, string name)
    {
        Text = $"{name}:\n";
        _name = name;
        _text = text;
        IsPrinting = true;
    }

    public void StopPrinting()
    {
        Text = _name+":\n"+_text;
        _counter = 0;
        IsPrinting = false;
    }
}
 