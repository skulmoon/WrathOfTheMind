using Godot;
using System;

public partial class BoneShard : ShardAbility
{
    public BoneShard(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, int maxRange) : base(zeroHealth, health, damage, speed, timeReload, critChance, maxRange)
    {
        Sprite.Texture = GD.Load<Texture2D>("res://Data/Textures/Entities/Shards/BoneShard.png");
        Light.Color = new Color("796f8c");
        Particles.Add(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardParticle1.tscn").Instantiate<GpuParticles2D>());
        Particles.Add(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardParticle2.tscn").Instantiate<GpuParticles2D>());
        foreach (var particle in Particles)
            AddChild(particle);
    }

    public override void Ability1()
    {
        for (int i = 0; i < 3; i++)
        {
            BoneShardProjectile2 projectile = new BoneShardProjectile2(Health / 2, Damage / 2, CritChance, Mathf.DegToRad(10), Vector2.FromAngle(Sprite.Rotation + Mathf.DegToRad(-45)), GlobalPosition);
            GetTree().CurrentScene.AddChild(projectile);
        }
    }

    public override void Ability2()
    {
        for (int i = 0; i < 6; i++)
        {
            BoneShardProjectile1 projectile = new BoneShardProjectile1(Health / 2, Damage / 2, CritChance, i * MathF.PI / 3);
            AddChild(projectile);
        }
    }

    public override float Attack()
    {
        float result = Health * Damage;
        result *= GD.Randf() > CritChance ? 2 : 1;
        Destroy();
        return result;
    }

    public override string[] GetAbilityNames() =>
        ["Calcium Throwing", "Circling Remains"];
}
