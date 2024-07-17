using Godot;
using System;

public partial class PlayerInteractionArea : Area2D
{
    private Vector2 _playerDirection = new(0, 1);

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
}
