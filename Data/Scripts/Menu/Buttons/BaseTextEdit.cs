using Godot;
using System;

public partial class BaseTextEdit : LineEdit
{
    private TextureRect _textureRect;

    [Export] public HorizontalSize HorizontalSize { get; set; }
    [Export] public Texture2D Normal { get; set; }
    [Export] public Texture2D Focused { get; set; }

    public override void _Ready()
    {
        _textureRect = GetNode<TextureRect>("TextureRect");
        OnFocusExited();
        OffsetLeft = -Size.Y * HorizontalSize.AsMultiplier() / 2;
        OffsetRight = Size.Y * HorizontalSize.AsMultiplier() / 2;
        AddThemeFontSizeOverride("font_size", ((int)Mathf.Round(Size.Y) - 7) / 16 * 16 );
        ((StyleBoxFlat)GetThemeStylebox("normal")).SetExpandMarginAll(Size.Y / 16);
        ((StyleBoxFlat)GetThemeStylebox("focus")).SetExpandMarginAll(Size.Y / 16);
    }

    public void OnFocusEntered() =>
        _textureRect.Texture = Focused;

    public void OnFocusExited() =>
        _textureRect.Texture = Normal;
}
