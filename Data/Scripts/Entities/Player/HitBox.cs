using Godot;
using System;

public partial class HitBox : Area2D
{
	public void TakeDamage(int damage)
	{
		GD.Print("Player is take damage.");
	}
}
