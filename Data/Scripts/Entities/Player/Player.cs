using Godot;
using System;

public partial class Player : CharacterBody2D
{
    private PlayerInteractionArea _interactionArea;
    private float _power = 0;

    public ShardManager Shard { get; private set; }
    public HitBox HitBox { get; private set; }
    public float Power 
    { 
        get => _power;
        set
        {
            _power = value;
            ChangedPower?.Invoke(_power);
        }
    }
    [Export] public float MaxPower { get; set; } = 100;
    [Export] public int Speed { get; set; } = 6000;
    [Export] public float Acceleration { get; set; } = 2;

    public event Action<float> ChangedPower;

    public override void _Ready()
    {
        _interactionArea = GetNode<PlayerInteractionArea>("PlayerInteractionArea");
        HitBox = GetNode<HitBox>("HitBox");
        Shard = new ShardManager(this);
        AddChild(Shard);
        Global.SceneObjects.Player = this;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move(delta);
    }

    private void Move(double delta)
    {
        float speedMultiper = 1;
        if (Input.IsActionPressed("acceleration") && Power - (float)delta > 0)
        {
            speedMultiper *= Acceleration;
            Power -= (float)delta * 40;
        }
        else if (!Input.IsActionPressed("acceleration") && Power < MaxPower)
            Power += (float)delta * 20;
        Vector2 direction = new Vector2(Input.GetAxis("left", "right"), Input.GetAxis("up", "down")).Normalized();
        Velocity = direction * Speed * speedMultiper * (float)delta;
        MoveAndSlide();
        _interactionArea.PayerDirection = direction;
    }  
}
