using Godot;
using System;
using System.Collections.Generic;
using static Godot.HttpRequest;

public partial class Test1Shard2D : ShardAbility
{
    public Test1Shard2D(Action<Shard2D> zeroHealth, Shard shard, int number) : base(zeroHealth, shard, number)
    {
        Light.Color = new Color(0, 1, 1);
    }

    public override string[] GetAbilityNames() =>
        ["FirstAbility", "SecondAbility"];

    public override List<PlayerAttack> Ability1()
    {
        GD.Print("ShardAbility1");
        return new List<PlayerAttack>();
    }
    
    public override List<PlayerAttack> Ability2()
    {
        GD.Print("ShardAbility2");
        return new List<PlayerAttack>();
    }
}
