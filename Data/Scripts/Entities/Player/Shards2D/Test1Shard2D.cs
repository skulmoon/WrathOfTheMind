using Godot;
using System;
using static Godot.HttpRequest;

public partial class Test1Shard2D : ShardAbility
{
    public int Number { get; set; }

    public Test1Shard2D(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, float maxRange) : base(zeroHealth, health, damage, speed, timeReload, critChance, maxRange) { }

    public override int Attack()
    {
        int result = (int)MathF.Round(Health * Damage);
        result *= GD.Randf() > CritChance ? 2 : 1;
        ResetHealth();
        return result;
    }

    public override string GetFormula() =>
        "Health * Damage";

    public override void Ability1()
    {
        //GD.Print(1);
    }
    
    public override void Ability2()
    {
        //GD.Print(2);
    }
}
