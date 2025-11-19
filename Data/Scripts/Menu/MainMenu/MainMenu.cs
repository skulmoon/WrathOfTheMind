using Godot;
using System;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
		if (!Global.Music.CheckMusic("CrystalmaniaV3.mp3"))
			Global.Music.PlayMusic("CrystalmaniaV3.mp3");
        UIDark dark = GetNode<UIDark>("%Dark");
        dark.HideDark();
    }

    public override void _ExitTree()
    {
        if (Global.Music.CheckMusic("CrystalmaniaV3.mp3"))
            Global.Music.PlayMusic(null);
    }
}
