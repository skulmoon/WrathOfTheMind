﻿using Godot;
using System;

public partial class ButtonLoad : TextureButton
{
    public void OnPressed()
    {
        GetTree().ChangeSceneToFile("res://Data/Scenes/Menu/GameLoader.tscn");
    }
}
