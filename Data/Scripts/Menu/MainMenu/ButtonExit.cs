using Godot;
using System;

public partial class ButtonExit : CustomButton
{
    public ButtonExit()
    {
        Pressed += OnPressed;
    }

    public void OnPressed() =>
        GetTree().Quit();
}
