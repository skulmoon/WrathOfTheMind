using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("open_pause_menu"))
            Visible = !Visible;
    }
}
