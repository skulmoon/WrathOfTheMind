using Godot;
using System;

public partial class CharapterRoom : Location
{
	public override void _Ready()
	{
        GetTree().CurrentScene.CreateTween().TweenProperty(GetTree().CurrentScene.GetNode<TextureRect>("%Dark"), "modulate:a", 0, 1);
    }
}
