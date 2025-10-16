using Godot;
using System;

public partial class SecondArmorAbilityBar : AbilityBar
{
    public override void OnPlayerChanged(Player player)
    {
        player.HitBox.ChangedArmor += OnChangedArmor;
    }

    public void OnChangedArmor(Armor2D shard)
    {
        if (shard is ArmorAbility shardAbility)
        {
            SetAbilityName(shardAbility.GetAbilityNames()[1]);
            shardAbility.SecondAbilityReloadStarted += OnAbilityReloadStarted;
        }
        else
            Close();
    }
}
