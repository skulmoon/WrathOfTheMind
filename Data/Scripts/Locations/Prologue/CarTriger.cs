using Godot;
using System;

public partial class CarTriger : Area2D
{
	public void OnBodyEntered(Node2D body)
	{
		if (body is Player)
		{
			GetNode<PrologueCar>("PrologueCar").ProcessMode = ProcessModeEnum.Inherit;
		}
	}
}
