using Godot;
using System;

public partial class CloseButton : CustomButton
{
    private bool _isPressed = false;
    [Export] public Label Label { get; set; }
    [Export] public PauseMenu Menu { get; set; }

    public CloseButton()
    {
        ProcessMode = ProcessModeEnum.WhenPaused;
        VisibilityChanged += () => _isPressed = false;
    }

    public void OnPressed()
	{
        Global.SaveManager.SaveGame();
        Menu.Visible = false;
        GetTree().Paused = false;
        Global.Inventory.Clear();
        Global.SceneObjects.ChangeScene("res://Data/Scenes/Menu/MainMenu.tscn");
        Global.Settings.CutScene = false;
    }
}
