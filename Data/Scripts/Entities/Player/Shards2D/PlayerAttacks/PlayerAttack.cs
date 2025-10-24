using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public abstract partial class PlayerAttack : Area2D
{
    private int _health = 30;

    public List<GpuParticles2D> Particles = new List<GpuParticles2D>();
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

    public virtual void TakeDamage(int damage)
    {
        GD.Print(_health);
        Health -= damage;
    }

    public virtual void Destroy() 
    {
        CollisionLayer = 0;
        CollisionMask = 0;
        double maxLifetime = 0;
        foreach(var particle in Particles)
        {
            if (particle.Lifetime > maxLifetime)
                maxLifetime = particle.Lifetime;
            particle.Emitting = false;
        }
        GetTree().CreateTimer(maxLifetime).Timeout += QueueFree;
    }
}
