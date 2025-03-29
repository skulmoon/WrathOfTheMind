using Godot;
using System;

public partial class ButtonLoad : CustomButton
{
    [Export] public string Path { get; set; }

    public ButtonLoad() =>
        Pressed += OnPressed;

    public void OnPressed()
    {
        GetTree().ChangeSceneToFile(Path);
    }
}
