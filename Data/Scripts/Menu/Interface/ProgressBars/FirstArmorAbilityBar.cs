using Godot;
using System;

public partial class FirstArmorAbilityBar : AbilityBar
{
    public override void OnPlayerChanged(Player player)
    {
        player.HitBox.ChangedArmor += OnChangedArmor;
    }

    public void OnChangedArmor(Armor2D armor)
    {
        if (armor is ArmorAbility armorAbility)
        {
            SetAbilityName(armorAbility.GetAbilityNames()[0]);
            armorAbility.FirstAbilityReloadStarted += OnAbilityReloadStarted;
        }
        else
            Close();
    }
}
