using Godot;
using System;

public partial class PlayerInteractionArea : Area2D
{
    private Vector2 _playerDirection = new(0, 1);
    private IInteractionArea _interactionArea;

    public Vector2 PayerDirection
    {
        get => _playerDirection;
        set
        {
            if (_playerDirection != value && value != Vector2.Zero)
            {
                _playerDirection = value;
                Position = value * (Global.Settings.GridSize / 2); 
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact") && (_interactionArea != null) && !Global.Settings.CutScene)
        {
            _interactionArea.Interaction();
        }
    }

    public void OnAreaEntered(Area2D interactionArea)
    {
        _interactionArea = interactionArea as IInteractionArea;
    }

    public void OnAreaExited(Area2D interactionArea)
    {
        _interactionArea = null;
    }
}
