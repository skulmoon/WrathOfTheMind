using Godot;
using System;

public partial class ButtonOptions : TextureButton
{
	public void OnPressed()
	{
		GetTree().ChangeSceneToFile("res://Data/Scenes/Menu/Options/Options.tscn");
	}
}
