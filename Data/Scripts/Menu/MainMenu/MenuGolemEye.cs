using Godot;
using System;

public partial class MenuGolemEye : TextureRect
{
	private Vector2 _startPosition;
    private Vector2 _windowSize;

    public MenuGolemEye() =>
        _startPosition = Position;

    public override void _Ready()
    {
    }

    public override void _Process(double delta)
	{
        Vector2 mousePosition = GetLocalMousePosition();
        mousePosition.Y *= 1.05f;
        Position = _startPosition + (mousePosition - GetWindow().Size / 2) * 0.01f;
    }
}
