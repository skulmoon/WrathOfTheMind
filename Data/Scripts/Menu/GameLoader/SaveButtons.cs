using Godot;
using System;

public delegate void Something();

public partial class SaveButtons : Button
{
    [Signal] public delegate void CurrentSaveNameEventHandler(string name);

    public override void _Ready()
    {
        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        EmitSignal(SignalName.CurrentSaveName, Name);
    }
}
