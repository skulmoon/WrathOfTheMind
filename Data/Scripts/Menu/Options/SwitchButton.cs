using Godot;
using System;

public partial class SwitchButton : CustomButton
{
    [Export] public Control[] Controls { get; set; }
    [Export] public Control MainControl { get; set; }
    [Export] public bool IsMain { get; set; }

    public override void _Ready()
    {
        ButtonDown += OnButtonPressed;
        if (IsMain)
        {
            OnButtonPressed();
            GrabFocus();
        }
        base._Ready();
    }

    public void OnButtonPressed()
    {
        foreach (var control in Controls)
            control.Visible = false;
        MainControl.Visible = true;
    }
}
