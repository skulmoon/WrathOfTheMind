using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public abstract partial class PlayerAttack : Area2D
{
    private int _health = 30;

    public List<GpuParticles2D> Particles = new List<GpuParticles2D>();
    public List<DirectedParticle> EndParticles = new List<DirectedParticle>();
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
        }
    }

    public PlayerAttack(int health, float damage, float critChance, bool defaultCollision = true)
    {
        CollisionLayer = 16;
        CollisionMask = 8 + 16;
        if(defaultCollision)
            AddChild(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/CollisionShape2D.tscn").Instantiate());
        Health = health;
        Damage = damage;
        CritChance = critChance;
    }

    public abstract float Attack();

    public virtual float Attack (Enemy enemy)
    {
        CreateEndParticles(-GlobalPosition.DirectionTo(enemy.GlobalPosition));
        return Attack();
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public virtual void TakeDamage(int damage, EnemyAttack attack)
    {
        TakeDamage(damage);
        if (Health <= 0)
            CreateEndParticles(-GlobalPosition.DirectionTo(attack.GlobalPosition));
    }

    public virtual double Disable()
    {
        CollisionLayer = 0;
        CollisionMask = 0;
        double maxLifetime = 0;
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

    public void CreateEndParticles(Vector2 Direction)
    {
        foreach (DirectedParticle particle in EndParticles)
        {
            GetTree().CurrentScene.AddChild(particle);
            particle.GlobalPosition = GlobalPosition;
            particle.Direction = Direction;
            particle.Emitting = true;
        }
    }
}
