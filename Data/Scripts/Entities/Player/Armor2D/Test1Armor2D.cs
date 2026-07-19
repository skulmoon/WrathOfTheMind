using Godot;
using System;
using System.Collections.Generic;

public partial class Test1Armor2D : ArmorAbility
{
    public Test1Armor2D(float protection, int additionalHealth) : base(protection, additionalHealth) { }

    public override string[] GetAbilityNames() =>
        ["FirstAbility", "SecondAbility"];

    public override List<PlayerAttack> Ability1()
	{
		GD.Print("ArmorAbility1");
        return new List<PlayerAttack>();
	}

	public override List<PlayerAttack> Ability2()
	{
        GD.Print("ArmorAbility2");
        return new List<PlayerAttack>();
    }
}
