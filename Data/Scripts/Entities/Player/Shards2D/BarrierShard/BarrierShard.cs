using Godot;
using System;

public partial class BarrierShard : ShardAbility
{
    public BarrierShard(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, int maxRange) : base(zeroHealth, health, damage, speed, timeReload, critChance, maxRange)
    {
        Light.Color = new Color("6d49a5");
    }

    public override void Ability1()
    {
        throw new NotImplementedException();
    }

    public override void Ability2()
    {
        throw new NotImplementedException();
    }

    public override float Attack()
    {
        float result = Health * Damage;
        result *= GD.Randf() > CritChance ? 2 : 1;
        Destroy();
        return result;
    }

    public override string[] GetAbilityNames()
    {
        throw new NotImplementedException();
    }
}
