using Godot;
using System;

public partial class MenuGolemEye : TextureRect
{
	private Vector2 _startPosition;

    public MenuGolemEye()
    {
        _startPosition = Position;
    }

    public override void _Process(double delta)
	{
        Position = _startPosition + (GetLocalMousePosition() - GetWindow().Size / 2) * 0.01f;
    }
}
