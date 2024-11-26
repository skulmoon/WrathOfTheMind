using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private PlayerInteractionArea _interactionArea;

    public PlayerInventory Inventory { get; set; }
    [Export] public Vector2 TargetPosition { get; set; }
    [Export] public float Speed { get; set; } = 6000;
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
        if (!Global.Settings.CutScene)
            Move(delta);
    }

    private void Move(double delta)
    {
        if (Input.IsActionPressed("acceleration"))
            delta *= Acceleration;
        Vector2 direction = new Vector2(Input.GetAxis("left", "right"), Input.GetAxis("up", "down")).Normalized();
        Velocity = direction * Speed * (float)delta;
        MoveAndSlide();
        _interactionArea.PayerDirection = direction;
    }  
}
