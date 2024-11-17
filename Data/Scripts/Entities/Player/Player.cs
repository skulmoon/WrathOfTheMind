using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private PlayerInteractionArea _interactionArea;
    private Vector2 _startPosition;
    private Vector2 _keyVector;
    private string _pressedKey = "nothing";
    private bool _isMoving = false;

    public PlayerInventory Inventory { get; set; }
    [Export] public Vector2 TargetPosition { get; set; }
    [Export] public float Speed { get; set; } = 100;
    [Export] public float Acceleration { get; set; } = 2;

    public override void _Ready()
    {
        _interactionArea = GetNode<PlayerInteractionArea>("PlayerInteractionArea");
        Global.SceneObjects.Player = this;
        Inventory = new PlayerInventory();
        Global.SceneObjects.TakePlayerSettings(this);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_isMoving)
        {
            MoveTowardsTarget(delta);
        }
        else if (!Global.Settings.CutScene)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        if (Input.IsActionPressed("up") && ("up" != _pressedKey))
        {
            SetPressedKey(Vector2.Up, "up");
            SetTargetPosition(Vector2.Up);
        }
        else if (Input.IsActionPressed("down") && ("down" != _pressedKey))
        {
            SetPressedKey(Vector2.Down, "down");
            SetTargetPosition(Vector2.Down);
        }
        else if (Input.IsActionPressed("left") && ("left" != _pressedKey))
        {
            SetPressedKey(Vector2.Left, "left");
            SetTargetPosition(Vector2.Left);
        }
        else if (Input.IsActionPressed("right") && ("right" != _pressedKey))
        {
            SetPressedKey(Vector2.Right, "right");
            SetTargetPosition(Vector2.Right);
        }
        else if (Input.IsActionPressed(_pressedKey))
        {
            SetTargetPosition(_keyVector);
        }
    }

    private void MoveTowardsTarget(double delta)
    {
        if (Input.IsActionPressed("acceleration"))
            delta *= Acceleration;

        Vector2 direction = (TargetPosition - Position).Normalized();
        var collided = MoveAndCollide(direction * Speed * (float)delta);

        if (collided != null)
        {
            _isMoving = false;
            TargetPosition = _startPosition;
            Position = _startPosition;
        }

        if (Position.DistanceTo(TargetPosition) < Speed * (float)delta)
        {
            Position = TargetPosition;
            _isMoving = false;
        }
    }

    private void SetPressedKey(Vector2 keyVector, string pressedKey)
    {
        if (!(Input.IsActionPressed(_pressedKey)))
        {
            _pressedKey = pressedKey;
            _keyVector = keyVector;
        }
    }

    private void SetTargetPosition(Vector2 direction)
    {
        _isMoving = true;
        TargetPosition += direction * Global.Settings.GridSize; 
        _startPosition = Position;
        _interactionArea.PayerDirection = direction;
    }   
}
