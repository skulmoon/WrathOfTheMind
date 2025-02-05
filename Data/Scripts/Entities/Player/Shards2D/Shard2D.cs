using Godot;
using System;

public abstract partial class Shard2D : Area2D
{
    private Sprite2D _sprite;
    private float _health = 30;
    private float _maxHealth = 30;

    public event Action<Shard2D> ZeroHealth = (x) => { };
    public PointLight2D Light { get; private set; }
    public float Damage { get; set; } = 20;
    public float Speed { get; set; } = 300;
    public float TimeReload { get; set; } = 1;
    public float CritChance { get; set; } = 0.2f;
    public float MaxRange { get; private set; } = 100;
    public float Health
    {
        get => _health;
        private set
        {
            if (value <= 0)
                ZeroHealth.Invoke(this);
            else
                _health = value;
        }
    }
    
    public Shard2D(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, float maxRange)
    {
        ZeroHealth += zeroHealth;
        CollisionLayer = 16;
        CollisionMask = 8 + 16;
        _sprite = (Sprite2D)GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Sprite2D.tscn").Instantiate();
        Light = (PointLight2D)GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/PointLight2D.tscn").Instantiate();
        AddChild(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/CollisionShape2D.tscn").Instantiate());
        AddChild(_sprite);
        AddChild(Light);
        _maxHealth = health;
        Health = health;
        Damage = damage;
        Speed = speed;
        TimeReload = timeReload;
        CritChance = critChance;
        MaxRange = maxRange;
    }

    public abstract float Attack();
    
    public void RecoveryHealth() =>
        _health = _maxHealth;

    public void ResetHealth() =>
        Health = 0;

    public virtual void TakeDamage(int damage)
    {
        GD.Print(_health);
        Health -= damage;
    }
}