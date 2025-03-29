using Godot;
using System;

public partial class CustomButton : TextureButton
{
    private bool _isButtonDown = false;
    private bool _isMouseEntered = false;
    private Texture2D _textureFocused;
    private Label _label = new Label();
    [Export] public string Text { get => _label.Text; set => _label.Text = value; }

    public override void _Ready()
    {
        _textureFocused = TextureFocused;
        _label.SetAnchorsPreset(LayoutPreset.FullRect);
        _label.HorizontalAlignment = HorizontalAlignment.Center;
        _label.VerticalAlignment = VerticalAlignment.Center;
        _label.LabelSettings = new LabelSettings
        {
            Font = GD.Load<Font>("res://Data/Textures/ComicoroRu_0.ttf"),
            FontSize = CalculateFontSize(),
        };
        AddChild(_label);
        OnButtonUp();
        ButtonDown += OnButtonDown;
        ButtonUp += OnButtonUp;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        base._Ready();
    }

    private int CalculateFontSize()
    {
        int fontSize = Mathf.RoundToInt(Size.Y / 1.2f) / 16 * 16;
        return fontSize > 0 ? fontSize : 16;
    }

    public void OnButtonDown()
    {
        _isButtonDown = true;
        if (_isMouseEntered)
        {
            UpdateLabelLayout(0.2f, 1);
            TextureFocused = TexturePressed;
        }
    }

    public void OnButtonUp()
    {
        _isButtonDown = false;
        UpdateLabelLayout(0, 0.9f);
        TextureFocused = _textureFocused;
    }

    public void OnMouseEntered()
    {
        _isMouseEntered = true;
        if (_isButtonDown)
        {
            UpdateLabelLayout(0.2f, 1);
            TextureFocused = TexturePressed;
        }
    }

    public void OnMouseExited()
    {
        _isMouseEntered = false;
        UpdateLabelLayout(0, 0.9f);
        TextureFocused = _textureFocused;
    }

    private void UpdateLabelLayout(float top, float bottom)
    {
        RemoveChild(_label);
        _label.SetAnchor(Side.Top, top);
        _label.SetAnchor(Side.Bottom, bottom);
        AddChild(_label);
    }
}
