using Godot;
using System;

public partial class CameraNPC : NPC
{
    public Camera2D Camera { get; private set; } = new Camera2D();

    [Export] public Vector2 Offset { get; set; }
    [Export] public Vector2 DefaultZoom { get; set; }

    public CameraNPC()
    {
        AddChild(Camera);
    }

    public override void _Ready()
    {
        base._Ready();
        Camera.Zoom = DefaultZoom;
        Camera.Offset = Offset;
    }

    public void ChangeZoom(float zoom, float duration)
    {
        CreateTween().TweenProperty(Camera, "zoom", new Vector2(zoom, zoom), duration).SetTrans(Tween.TransitionType.Sine);
    }

    public void ChangeEnabled (bool enabled) =>
        Camera.Enabled = enabled;
}
