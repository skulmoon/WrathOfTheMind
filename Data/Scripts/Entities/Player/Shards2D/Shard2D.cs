using Godot;
using System;

public abstract partial class Shard2D : PlayerAttack
{
    private int _health = 30;
    private int _maxHealth = 30;

    public event Action<Shard2D> ZeroHealth = (x) => { };
    public Sprite2D Sprite { get; set; }
    public PointLight2D Light { get; private set; }
    public int Speed { get; set; } = 300;
    public float TimeReload { get; set; } = 1;
    public int MaxRange { get; private set; } = 100;
    
    public Shard2D(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, int maxRange) : base(health, damage, critChance)
    {
        ZeroHealth += zeroHealth;
        Sprite = (Sprite2D)GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Sprite2D.tscn").Instantiate();
        AddChild(Sprite);
        Light = (PointLight2D)GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/PointLight2D.tscn").Instantiate();
        AddChild(Light);
        _maxHealth = health;
        Health = health;
        Damage = damage;
        Speed = speed;
        TimeReload = timeReload;
        CritChance = critChance;
        MaxRange = maxRange;
    }

    public override void Destroy()
    {
        ZeroHealth.Invoke(this);
        _health = 0;
    }

    public void RecoveryHealth() =>
        _health = _maxHealth;
}