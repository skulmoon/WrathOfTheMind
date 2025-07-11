using Godot;
using System;

public partial class LerningTakeItemTrigger : Area2D
{
	public void OnBodyEntered(Node2D node)
	{
		if (node is Player)
		{
			GD.Print("Trigger is worked!");
		}
	}
}
