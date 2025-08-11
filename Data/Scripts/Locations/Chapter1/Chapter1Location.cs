using Godot;
using System;

public partial class Chapter1Location : Location
{
	public override void _Ready()
	{
		AddChild(new EnemyCoreCharapter1());
		base._Ready();
	}
}
