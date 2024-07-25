using Godot;
using System;

public partial class PlayerInteractionArea : Area2D
{
    private Vector2 _playerDirection = new(0, 1);
    private NPCInteractionArea _interactionArea;

    public Vector2 PayerDirection
    {
        get
        {
            return _playerDirection;
        }
        set
        {
            _playerDirection = value;
            Position = value * (Global.Settings.GridSize / 2);
        }
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("interact") && (_interactionArea != null))
        {
                _interactionArea.InteractionWithNPC();
        }
    }

    public void OnAreaEntered(Area2D interactionArea)
    {
        _interactionArea = interactionArea as NPCInteractionArea;
    }

    public void OnAreaExited(Area2D interactionArea)
    {
        _interactionArea = null;
    }
}
