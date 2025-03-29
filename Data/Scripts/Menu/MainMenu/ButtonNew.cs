using Godot;
using System;

public partial class ButtonNew : CustomButton
{
    public void OnPressed()
    {
        GetTree().ChangeSceneToFile("res://Data/Scenes/Menu/GameCreator.tscn");
    }
}
