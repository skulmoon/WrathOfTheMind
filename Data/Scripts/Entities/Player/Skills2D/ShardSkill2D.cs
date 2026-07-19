using Godot;
using System;
using System.Collections.Generic;

public partial class ShardSkill2D : Skill2D
{
    public ShardSkill2D(Shard2D shard) : base(shard)
    {
        shard.Destroyed += OnDestroyed;
        shard.Initialized += OnInitialized;
        if (shard is ShardAbility shardAbility)
        {
            shardAbility.FirstAbilityUssed += OnAbilityUssed;
            shardAbility.SecondAbilityUssed += OnAbilityUssed;
        }
    }

    protected virtual void OnDestroyed(Shard2D shard) { }
    protected virtual void OnInitialized(Shard2D shard) { }
    protected virtual void OnAbilityUssed(Shard2D shard, float reloadTime, List<PlayerAttack> attacks) { }
}
