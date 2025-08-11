using Godot;
using System;

public partial class MenuGolem : TextureRect
{
    private Vector2 _startPosition;

    public MenuGolem()
    {
        _startPosition = Position;
    }
    public override void _Process(double delta)
	{
        Position = _startPosition - (GetLocalMousePosition() - GetWindow().Size / 2) * 0.005f;
    }
}
