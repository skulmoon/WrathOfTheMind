using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
	[Export] public Label Label { get; set; }

    public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("open_pause_menu"))
		{
			Visible = !Visible;
			Global.Settings.CutScene = Visible;
			GetTree().Paused = Visible;
            Label.Text = string.Empty;
		}
	}
}
