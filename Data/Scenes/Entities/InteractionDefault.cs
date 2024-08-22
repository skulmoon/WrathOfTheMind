using Godot;
using System;

public partial class InteractionDefault : Area2D, IInteractionArea
{
	public void Interaction()
	{
		GD.Print("You forgot to change script, stupid idiot!");
	}
}
