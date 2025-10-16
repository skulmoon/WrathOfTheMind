using Godot;
using System;

public partial class Test1Armor2D : ArmorAbility
{
    public Test1Armor2D(float protection, int additionalHealth) : base(protection, additionalHealth) { }

    public override string[] GetAbilityNames() =>
        ["FirstAbility", "SecondAbility"];

    public override void Ability1()
	{
		GD.Print("ArmorAbility1");
	}

	public override void Ability2()
	{
        GD.Print("ArmorAbility2");
    }
}
