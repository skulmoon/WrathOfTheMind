using Godot;
using System;

public partial class SaveButton : TextureButton
{
    [Signal] public delegate void CurrentSaveNameEventHandler(string name);

    private Label _label;

    public string Text { get => _label.Text; set => _label.Text = value; }

    public SaveButton()
    {
        _label = new Label();
        _label.SetAnchorsPreset(LayoutPreset.Center);
        StretchMode = StretchModeEnum.KeepAspectCentered;
        CustomMinimumSize = new Vector2(0, 120);
        TextureNormal = GD.Load<Texture2D>("res://Data/Textures/Menu/Buttons/LongButton/NormalLongButton.png");
        TextureHover = GD.Load<Texture2D>("res://Data/Textures/Menu/Buttons/LongButton/HoverLongButton.png");
        TexturePressed = GD.Load<Texture2D>("res://Data/Textures/Menu/Buttons/LongButton/PressedLongButton.png");
        TextureFocused = GD.Load<Texture2D>("res://Data/Textures/Menu/Buttons/LongButton/HoverLongButton.png");
    }

    public override void _Ready()
    {
        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        EmitSignal(SignalName.CurrentSaveName, Name);
    }
}
