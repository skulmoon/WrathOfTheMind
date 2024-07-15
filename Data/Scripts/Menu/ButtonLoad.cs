using Godot;
using System;

public partial class ButtonLoad : Button
{
    public void OnPressed()
    {
        GetTree().ChangeSceneToFile("res://Data/Scenes/Location/Test/Test.tscn");
    }
}
