using Godot;
using System;

public partial class BoneShardProjectile2 : PlayerAttack
{
    public Sprite2D Sprite { get; set; }
    public float LifeTime { get; set; } = 1;
    public Vector2 Direction { get; set; }
    public float Speed { get; set; } = 300;

    public BoneShardProjectile2(int health, float damage, float critChance, float spreading, Vector2 direction, Vector2 startPosition, bool defaultCollision = true) : base(health, damage, critChance, defaultCollision)
    {
        AddParticle((GpuParticles2D)GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardProgectileParticles1.tscn").Instantiate());
        EndParticles.Add(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardProgectileParticlesDestroyed.tscn").Instantiate<DirectedParticle>());
        Sprite = new Sprite2D();
        Sprite.Texture = GD.Load<Texture2D>($"res://Data/Textures/Entities/Shards/BoneShard/BoneShardProjectile{GD.Randi() % 3 + 1}.png");
        AddChild(Sprite);
        Timer timer = new Timer()
        {
            WaitTime = LifeTime,
            Autostart = true,
            OneShot = true,
        };
        AddChild(timer);
        timer.Timeout += () => Destroy();
        GlobalPosition = startPosition;
        Direction = direction.Normalized().Rotated((float)GD.RandRange(-spreading / 2, spreading / 2));
        CreateTween().TweenProperty(this, "Speed", 1, LifeTime);
        Tween tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 1, LifeTime - 0.5f);
        tween.Chain();
        tween.TweenProperty(this, "modulate:a", 0, 0.5f);
    }

    public override void _Process(double delta)
    {
        Position += Direction * Speed * (float)delta;
    }

    public override float Attack()
    {
        float result = Health * Damage;
        result *= GD.Randf() > CritChance ? 2 : 1;
        Destroy();
        return result;
    }

    public override void Destroy()
    {
        if (Sprite != null)
            Sprite.Visible = false;
        base.Destroy();
    }
}
