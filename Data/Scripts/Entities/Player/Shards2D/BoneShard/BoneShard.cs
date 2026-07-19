using Godot;
using System;
using System.Collections.Generic;

public partial class BoneShard : ShardAbility
{
    public BoneShard(Action<Shard2D> zeroHealth, Shard shard, int number) : base(zeroHealth, shard, number)
    {
        Sprite.Texture = GD.Load<Texture2D>("res://Data/Textures/Entities/Shards/BoneShard.png");
        Light.Color = new Color("796f8c");
        AddParticle(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardParticle1.tscn").Instantiate<GpuParticles2D>());
        AddParticle(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardParticle2.tscn").Instantiate<GpuParticles2D>());
        EndParticles.Add(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/BoneShard/BoneShardProgectileParticlesDestroyed.tscn").Instantiate<DirectedParticle>());
    }

    public override List<PlayerAttack> Ability1()
    {
        List<PlayerAttack> list = new List<PlayerAttack>();
        for (int i = 0; i < 3; i++)
        {
            BoneShardProjectile2 projectile = new BoneShardProjectile2(MaxHealth / 2, Damage / 2, CritChance, Mathf.DegToRad(10), Vector2.FromAngle(Sprite.Rotation + Mathf.DegToRad(-45)), GlobalPosition);
            list.Add(projectile);
            GetTree().CurrentScene.AddChild(projectile);
        }
        TakeDamage(10);
        return list;
    }

    public override List<PlayerAttack> Ability2()
    {
        List<PlayerAttack> list = new List<PlayerAttack>();
        ProjectileContainer container = new ProjectileContainer();
        AddChild(container);
        for (int i = 0; i < 6; i++)
        {
            BoneShardProjectile1 projectile = new BoneShardProjectile1(MaxHealth / 2, Damage / 2, CritChance, i * MathF.PI / 3);
            container.AddChild(projectile);
            list.Add(projectile);
        }
        TakeDamage(20);
        return list;
    }

    public override double Disable()
    {
        foreach (var node in GetChildren())
            if (node is ProjectileContainer node2D)
            {
                RemoveChild(node2D);
                GetTree().CurrentScene.CallDeferred("add_child", node2D);
                node2D.GlobalPosition = GlobalPosition;
            }
        return base.Disable();
    }

    public override string[] GetAbilityNames() =>
        ["Calcium Throwing", "Circling Remains"];
}
