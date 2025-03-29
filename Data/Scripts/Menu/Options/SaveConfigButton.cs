using Godot;
using System;
using System.IO;

public partial class SaveConfigButton : CustomButton
{
    [Export] public OptionsMenu Options { get; set; }

    public SaveConfigButton() =>
        Pressed += OnPressed;

    public void OnPressed()
    {
        Options.SetConfigInfo();
    }
}
