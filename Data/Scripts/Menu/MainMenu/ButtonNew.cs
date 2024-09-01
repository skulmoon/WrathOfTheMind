using Godot;
using System;

public partial class ButtonNew : Button
{
    public void OnPressed()
    {
        GetTree().ChangeSceneToFile("res://Data/Scenes/Menu/GameCreator.tscn");
    }
}
