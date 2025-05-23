using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private PlayerInteractionArea _interactionArea;
    private PlayerInventory _inventory;

    public ShardManager Shard { get; private set; }
    public PlayerInventory Inventory { get => _inventory; private set => _inventory = value; }
    [Export] public float Speed { get; set; } = 6000;
    [Export] public float Acceleration { get; set; } = 2;

    public override void _Ready()
    {
        _interactionArea = GetNode<PlayerInteractionArea>("PlayerInteractionArea");
        Inventory = new PlayerInventory();
        Shard = new ShardManager(this);
        AddChild(Shard);
        Global.SceneObjects.TakePlayerSettings(this);
        Global.SceneObjects.Player = this;
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
