using Godot;
using System;

public partial class ButtonNew : TextureButton
{
    public void OnPressed()
    {
        GetTree().ChangeSceneToFile("res://Data/Scenes/Menu/GameCreator.tscn");
    }
}
