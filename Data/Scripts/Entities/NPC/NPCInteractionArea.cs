using Godot;
using System;

public partial class NPCInteractionArea : Area2D
{
    private bool _active = false;

    public override void _Process(double delta)
    {
        if (_active && Input.IsActionJustPressed("interact"))
        {
            GD.Print("Interaction with NPCs.");
        }
    }

    public void OnAreaEntered(Area2D something)
    {
        _active = true;
    }

    public void OnAreaExited(Area2D something)
    {
        _active = false;
    }
}
