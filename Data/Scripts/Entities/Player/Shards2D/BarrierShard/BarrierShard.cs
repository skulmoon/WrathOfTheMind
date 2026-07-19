using Godot;
using System;
using System.Collections.Generic;

public partial class BarrierShard : ShardAbility
{
    public BarrierShard(Action<Shard2D> zeroHealth, Shard shard, int number) : base(zeroHealth, shard, number)
    {
        Light.Color = new Color("6d49a5");
    }

    public override List<PlayerAttack> Ability1()
    {
        throw new NotImplementedException();
    }

    public override List<PlayerAttack> Ability2()
    {
        throw new NotImplementedException();
    }

    public override string[] GetAbilityNames()
    {
        throw new NotImplementedException();
    }
}
