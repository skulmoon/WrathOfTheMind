using Godot;
using System;
using static Godot.HttpRequest;

public partial class Test1Shard2D : ShardAbility
{
    public Test1Shard2D(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, float maxRange) : base(zeroHealth, health, damage, speed, timeReload, critChance, maxRange)
    {
        Light.Color = new Color(0, 1, 1);
    }

    public override float Attack()
    {
        float result = Health * Damage;
        result *= GD.Randf() > CritChance ? 2 : 1;
        ResetHealth();
        return result;
    }

    public override void Ability1()
    {
        GD.Print(1);
    }
    
    public override void Ability2()
    {
        GD.Print(2);
    }
}
