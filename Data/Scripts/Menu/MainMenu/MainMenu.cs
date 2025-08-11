using Godot;
using System;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
		if (!Global.Music.CheckMusic("CrystalmaniaV2.mp3"))
			Global.Music.PlayMusic("CrystalmaniaV2.mp3");
	}
}
