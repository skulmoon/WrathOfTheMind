using Godot;
using System;

public abstract partial class Shard2D : PlayerAttack
{
    public int Number = 0;
    public Shard Item;
    public int MaxHealth { get; private set; } = 30;
    public virtual bool IsMain { get; set; } = false;
    public Sprite2D Sprite { get; set; }
    public PointLight2D Light { get; private set; }
    public int Speed { get; set; } = 300;
    public float TimeReload { get; set; } = 1;
    public int MaxRange { get; private set; } = 100;

    public event Action<Shard2D> Destroyed;
    public event Action<Shard2D> Initialized { add => value.Invoke(this); remove { } }
    public event RefAction<float, float, float, Shard2D> Attacked;

    public Shard2D(Action<Shard2D> zeroHealth, Shard shard, int number) : base(shard.Health, shard.Damage, shard.CritChance)
    {
        Number = number;
        Destroyed += zeroHealth;
        Sprite = (Sprite2D)GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Sprite2D.tscn").Instantiate();
        AddChild(Sprite);
        Light = (PointLight2D)GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/PointLight2D.tscn").Instantiate();
        Sprite.AddChild(Light);
        MaxHealth = shard.Health;
        Speed = shard.Speed;
        TimeReload = shard.TimeReload;
        CritChance = shard.CritChance;
        MaxRange = shard.MaxRange;
    }

    public void Destroy(bool isBaseDestroy = false)
    {
        if (isBaseDestroy)
            base.Destroy();
        else
            Disable();
    }

    public override void Destroy() =>
        Disable();

    public override double Disable()
    {
        double result = base.Disable();
        ResetHeath();
        Sprite.Visible = false;
        Destroyed?.Invoke(this);
        return result;
    }

    public override void Enable()
    {
        Health = MaxHealth;
        Sprite.Visible = true;
        base.Enable();
    }

    public override float Attack()
    {
        float result = Health * Damage;
        float critChance = CritChance;
        float critMultiplier = 2;
        Attacked?.Invoke(ref result, ref critChance, ref critMultiplier, this);
        result *= GD.Randf() > critChance ? critMultiplier : 1;
        Disable();
        return result;
    }

    public override void AddParticle(GpuParticles2D particle)
    {
        particle.Emitting = false;
        base.AddParticle(particle);
    }
}