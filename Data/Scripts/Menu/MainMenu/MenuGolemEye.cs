using Godot;
using System;

public partial class MenuGolemEye : TextureRect
{
	private Vector2 _startPosition;
    private Vector2 _windowSize;

    public MenuGolemEye() =>
        _startPosition = Position;

    public override void _Process(double delta)
	{
        Vector2 mousePosition = GetLocalMousePosition();
        mousePosition.Y *= 1.05f;
        Vector2 mouseVector = mousePosition - GetWindow().Size / 2;
        mouseVector *= 2;
        if (mouseVector.DistanceTo(Vector2.Zero) > GetWindow().Size.X / 2)
            mouseVector = mouseVector.Normalized() * GetWindow().Size.X / 2;
        Position = _startPosition + mouseVector * 0.008f;
    }
}
