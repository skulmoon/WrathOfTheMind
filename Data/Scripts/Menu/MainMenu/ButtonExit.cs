using Godot;
using System;

public partial class ButtonExit : CustomButton
{
    public void OnPressed() =>
        GetTree().Quit();
}
