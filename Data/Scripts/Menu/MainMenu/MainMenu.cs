using Godot;
using System;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
		if (!Global.Music.CheckMusic("CrystalmaniaV3.mp3"))
			Global.Music.PlayMusic("CrystalmaniaV3.mp3");
        TextureRect dark = GetNode<TextureRect>("%Dark");
        dark.Visible = true;
        Tween tween = CreateTween();
        tween.TweenProperty(dark, "modulate:a", 1, 0.2);
        tween.Chain();
        tween.TweenProperty(dark, "modulate:a", 0, 0.5);
    }

    public override void _ExitTree()
    {
        if (Global.Music.CheckMusic("CrystalmaniaV3.mp3"))
            Global.Music.PlayMusic(null);
    }
}
