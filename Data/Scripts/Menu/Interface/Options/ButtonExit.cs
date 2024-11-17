using Godot;
using System;

public partial class ButtonExit : Button
{
    public void OnPressed()
    {
        Global.Dialogue.SaveDialogues();
        Global.JSON.SaveGame();
        GetTree().ChangeSceneToFile("res://Data/Scenes/Menu/MainMenu.tscn");
    }
}
