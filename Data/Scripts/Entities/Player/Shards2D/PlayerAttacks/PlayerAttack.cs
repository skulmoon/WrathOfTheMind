using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public abstract partial class PlayerAttack : CharacterBody2D
{
    private int _health = 30;
    private bool _isSecond = false;

    public List<GpuParticles2D> Particles = new List<GpuParticles2D>();
    public List<DirectedParticle> EndParticles = new List<DirectedParticle>();
    public List<DirectedParticle> EndParticles2;

    public bool IsEnabled { get; set; } = true;
    public float Damage { get; set; } = 20;
    public float CritChance { get; set; } = 0.2f;
    public int Health
    {
        get => _health;
        set
        {
            if (value <= 0)
                Destroy();
            else
                _health = value;
            HealthChanged?.Invoke(value);
        }
    }

    public event Action<int> HealthChanged;

    public PlayerAttack(int health, float damage, float critChance, bool defaultCollision = true)
    {
        CollisionLayer = 16;
        CollisionMask = 8 + 16;
        if (defaultCollision)
            AddChild(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/CollisionShape2D.tscn").Instantiate());
        Health = health;
        Damage = damage;
        CritChance = critChance;
    }

    public override void _Ready()
    {
        EndParticles2 = EndParticles.Duplicate();
        base._Ready();
    }

    public abstract float Attack();

    public virtual float Attack (Enemy enemy)
    {
        Vector2 enemyCenter = enemy.GlobalPosition;
        enemyCenter.Y -= 32;
        CreateEndParticles(-GlobalPosition.DirectionTo(enemyCenter));
        return Attack();
    }

    public virtual void TakeDamage(int damage) =>
        Health -= damage;

    public virtual void TakeDamage(int damage, EnemyAttack attack)
    {
        if (Health - damage <= 0)
            CreateEndParticles(-GlobalPosition.DirectionTo(attack.GlobalPosition));
        TakeDamage(damage);
    }

    public virtual double Disable()
    {
        CollisionLayer = 0;
        CollisionMask = 0;
        double maxLifetime = 0;
        IsEnabled = false;
        foreach (var particle in Particles)
        {
            if (particle.Lifetime > maxLifetime)
                maxLifetime = particle.Lifetime;
            particle.Emitting = false;
        }
        return maxLifetime;
    }

    public virtual void Enable()
    {
        CollisionLayer = 16;
        CollisionMask = 8 + 16;
        IsEnabled = true;
        foreach (var particle in Particles)
            particle.Emitting = true;
    }

    public virtual void Destroy()
    {
        if (IsInsideTree())
            GetTree().CreateTimer(Disable()).Timeout += QueueFree;
    }

    public void ResetHeath() =>
        _health = 0;

    public virtual void AddParticle(GpuParticles2D particle)
    {
        Particles.Add(particle);
        AddChild(particle);
    }

    public void CreateEndParticles(Vector2 direction)
    {
        if (EndParticles.Count <= 0)
            return;
        List<DirectedParticle> endParticles = null;
        if (_isSecond)
            endParticles = EndParticles2;
        else
            endParticles = EndParticles;
        _isSecond = !_isSecond;
        foreach (DirectedParticle particle in endParticles)
        {
            if (particle.GetParent() == null)
                Global.SceneObjects.Location.AddChild(particle);
            particle.GlobalPosition = GlobalPosition - (direction * 8);
            particle.Direction = direction;
            particle.Emitting = true;
        }
    }
}
