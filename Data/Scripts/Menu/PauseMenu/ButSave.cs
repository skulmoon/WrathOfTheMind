using Godot;
using System;

public partial class ButSave : CustomButton

{
    [Export] public Label Label { get; set; }

    public ButSave()
    {
        ProcessMode = ProcessModeEnum.WhenPaused;
    }

    public void OnPressed()
	{
        Global.SaveManager.SaveGame();
    }
}
