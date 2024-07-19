using Godot;
using System;

public partial class PlayerInteractionArea : Area2D
{
    private Vector2 _playerDirection = new(0, 1);
    private NPCInteractionArea _interactionArea;

    [Export] public int GridSize { get; set; } = 32;

    public Vector2 PayerDirection
    {
        get
        {
            return _playerDirection;
        }
        set
        {
            _playerDirection = value;
            Position = value * (GridSize / 2);
        }
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("interact"))
        {
            try
            {
                _interactionArea.InteractionWithNPC();
            }
            catch { }
        }
    }

    public void OnAreaEntered(Area2D interactionArea)
    {
        try
        {
            _interactionArea = (NPCInteractionArea)interactionArea;
        }
        catch { }
    }

    public void OnAreaExited(Area2D interactionArea)
    {
        _interactionArea = null;
    }
}