using Godot;
using System;

public partial class BoneShardProjectile1 : PlayerAttack
{
    private const int MAX_DISTANCE = 80;

    public Sprite2D Sprite { get; set; }
    public int LifeTime { get; set; } = 5;
    public float RotationSpeed { get; set; } = MathF.PI;
    public float CurrentRotation { get; set; } = 0;
    public float CurrentDistance { get; set; } = 0;

    public BoneShardProjectile1(int health, float damage, float critChance, float StartRotation, bool defaultCollision = true) : base(health, damage, critChance, defaultCollision)
    {
        Particles.Add((GpuParticles2D)GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardProgectileParticles1.tscn").Instantiate());
        AddChild(Particles[0]);
        Sprite = new Sprite2D();
        Sprite.Texture = GD.Load<Texture2D>($"res://Data/Textures/Entities/Shards/BoneShard/BoneShardProjectile{GD.Randi() % 3 + 1}.png");
        AddChild(Sprite);
        CurrentRotation = StartRotation;
        CreateTween().TweenProperty(this, "CurrentRotation", CurrentRotation + (LifeTime * RotationSpeed), LifeTime);
        Tween DistanceTween = CreateTween();
        DistanceTween.SetEase(Tween.EaseType.Out);
        DistanceTween.TweenProperty(this, "CurrentDistance", MAX_DISTANCE, 1);
        DistanceTween.Chain();
        DistanceTween.SetEase(Tween.EaseType.InOut);
        DistanceTween.TweenProperty(this, "CurrentDistance", MAX_DISTANCE, LifeTime - 1 * 2);
        DistanceTween.Chain();
        DistanceTween.SetEase(Tween.EaseType.Out);
        DistanceTween.TweenProperty(this, "CurrentDistance", 0, 1);
        DistanceTween.TweenCallback(new Callable(this, "Destroy"));
    }

    public override void _Process(double delta)
    {
        Position = new Vector2(Mathf.Cos(CurrentRotation), Mathf.Sin(CurrentRotation)) * CurrentDistance;
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
        Sprite.Visible = false;
        base.Destroy(); 
    }
}
