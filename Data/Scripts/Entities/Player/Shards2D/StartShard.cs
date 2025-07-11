using Godot;
using System;

public partial class StartShard : Shard2D
{
    public StartShard(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, float maxRange) : base(zeroHealth, health, damage, speed, timeReload, critChance, maxRange)
    {
        Light.Color = new Color(0.2f, 1, 1);
    }

    public override float Attack()
    {
        float result = Health * Damage;
        result *= GD.Randf() > CritChance ? 2 : 1;
        ResetHealth();
        return result;
    }
}
