using Godot;
using System;

public partial class SecondShardAbilityBar : AbilityBar
{
    public override void OnPlayerChanged(Player player)
    {
        player.Shard.MainShard2DChanged += OnShardChanged;
    }

    public void OnShardChanged(Shard2D shard)
    {
        if (shard is ShardAbility shardAbility)
        {
            SetAbilityName(shardAbility.GetAbilityNames()[1]);
            shardAbility.SecondAbilityReloadStarted += OnAbilityReloadStarted;
        }
        else
            Close();
    }
}
