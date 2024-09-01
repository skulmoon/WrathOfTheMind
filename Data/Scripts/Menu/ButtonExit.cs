using Godot;
using System;

public partial class ButtonExit : Button
{
    public void OnPressed()
    {
        Global.DialogueManager.SaveDialogues();
        Global.JSONManager.SaveGame();
        GetTree().Quit();
    }
}
