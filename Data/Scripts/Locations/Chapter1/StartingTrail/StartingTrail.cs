using Godot;
using System;

public partial class StartingTrail : Chapter1Location
{
	public override void _Ready()
	{
		base._Ready();
		Global.Variables.CutScene = false;
	}
}
