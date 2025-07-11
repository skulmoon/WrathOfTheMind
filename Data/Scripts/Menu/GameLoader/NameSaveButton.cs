using Godot;
using System;

public partial class NameSaveButton : CustomButton
{
    [Signal] public delegate void CurrentSaveNameEventHandler(string name);

    public NameSaveButton()
    {
        StretchMode = StretchModeEnum.KeepAspectCentered;
        CustomMinimumSize = new Vector2(0, 120);
        TextureNormal = GD.Load<Texture2D>("res://Data/Textures/Menu/Buttons/LongButton/NormalLongButton.png");
        TextureHover = GD.Load<Texture2D>("res://Data/Textures/Menu/Buttons/LongButton/HoverLongButton.png");
        TexturePressed = GD.Load<Texture2D>("res://Data/Textures/Menu/Buttons/LongButton/PressedLongButton.png");
        TextureFocused = GD.Load<Texture2D>("res://Data/Textures/Menu/Buttons/LongButton/FocusedLongButton.png");
        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        EmitSignal(SignalName.CurrentSaveName, Name);
    }
}
