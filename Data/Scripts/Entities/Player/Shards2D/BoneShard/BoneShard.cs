using Godot;
using System;

public partial class BoneShard : ShardAbility
{
    public BoneShard(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, int maxRange) : base(zeroHealth, health, damage, speed, timeReload, critChance, maxRange)
    {
        Sprite.Texture = GD.Load<Texture2D>("res://Data/Textures/Entities/Shards/BoneShard.png");
        Light.Color = new Color("796f8c");
        AddParticle(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardParticle1.tscn").Instantiate<GpuParticles2D>());
        AddParticle(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardParticle2.tscn").Instantiate<GpuParticles2D>());
        EndParticles.Add(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardProgectileParticlesDestroyed.tscn").Instantiate<DirectedParticle>());
    }

    public override void Ability1()
    {
        for (int i = 0; i < 3; i++)
        {
            BoneShardProjectile2 projectile = new BoneShardProjectile2(Health / 2, Damage / 2, CritChance, Mathf.DegToRad(10), Vector2.FromAngle(Sprite.Rotation + Mathf.DegToRad(-45)), GlobalPosition);
            GetTree().CurrentScene.AddChild(projectile);
        }
        TakeDamage(10);
    }

    public override void Ability2()
    {
        ProjectileContainer container = new ProjectileContainer();
        AddChild(container);
        for (int i = 0; i < 6; i++)
        {
            BoneShardProjectile1 projectile = new BoneShardProjectile1(Health / 2, Damage / 2, CritChance, i * MathF.PI / 3);
            container.AddChild(projectile);
        }
        TakeDamage(20);
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
        foreach(var node in GetChildren())
            if (node is ProjectileContainer node2D)
            {
                RemoveChild(node2D);
                GetTree().CurrentScene.CallDeferred("add_child", node2D);
                node2D.GlobalPosition = GlobalPosition;
            }
        base.Destroy();
    }

    public override string[] GetAbilityNames() =>
        ["Calcium Throwing", "Circling Remains"];
}
