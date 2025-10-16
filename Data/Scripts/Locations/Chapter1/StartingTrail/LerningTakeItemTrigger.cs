using Godot;
using System;

public partial class LerningTakeItemTrigger : Area2D
{
    [Export] public LernMovementAnimetedButton LastMenu { get; set; }
    [Export] public LernTakeItemAnimetedButton Button { get; set; }

	public void OnBodyEntered(Node2D node)
	{
		if (node is Player)
		{
			Button.Activate();
			LastMenu.Hide();
        }
	}
}
