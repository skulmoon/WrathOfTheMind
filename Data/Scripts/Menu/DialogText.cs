using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class DialogText : RichTextLabel
{
    const int MAX_OPTIONS = 5;
	private Panel[] _panels = new Panel[MAX_OPTIONS];

    public Control Control { get; set; }
    public Label[] OptionsText { get; set; } = new Label[MAX_OPTIONS];
    public int CountOfOptions { get; set; } = 2;
    public int CurrentPosition { get; set; } = 0;

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
}
 