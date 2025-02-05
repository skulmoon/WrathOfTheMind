using Godot;
using System;

public partial class Test1Armor2D : ArmorAbility
{
    public Test1Armor2D(float protection, float additionalHealth) : base(protection, additionalHealth) { }

    public override void Ability1()
	{
		GD.Print("ArmorAbility1");
	}

	public override void Ability2()
	{
        GD.Print("ArmorAbility2");
    }
}
