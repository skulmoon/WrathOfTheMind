using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export] public Label Label { get; set; }

    public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("open_pause_menu"))
		{
			Visible = !Visible;
			Global.Variables.CutScene = Visible;
			GetTree().Paused = Visible;
			if (GetNode<InventoryMenu>("%InventoryMenu").Visible)
                GetTree().Paused = true;
            else
                GetTree().Paused = Visible;
            Label.Text = string.Empty;
		}
	}
}
