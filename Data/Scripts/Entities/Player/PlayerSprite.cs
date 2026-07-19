using Godot;
using System;

public partial class PlayerSprite : AnimatedSprite2D
{
    private string _direction = "down";
    private string _state = "standing";
    private Vector2 _currentDirection = Vector2.Down;

    public override void _Ready()
    {
        GetParent<Player>().ChangedDirection += OnChangeDirection;
        GetParent<Player>().SpeedMultiperChanged += OnChangedSpeedMultiper;
        Play($"{_state}_{_direction}");
    }

    public void OnChangeDirection(Vector2 direction)
    {
        _direction = direction == Vector2.Down ? "down" :
            direction == Vector2.Left ? "left" :
            direction == Vector2.Right ? "right" :
            direction == Vector2.Up ? "up" : _direction;
        if (direction == Vector2.Zero)
            _state = "standing";
        else
            _state = "movement";
        Play($"{_state}_{_direction}");
    }

    public void OnChangedSpeedMultiper(float multiper)
    {
        SpeedScale = multiper;
    }
}
