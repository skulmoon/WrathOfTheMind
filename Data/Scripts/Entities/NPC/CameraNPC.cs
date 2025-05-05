using Godot;
using System;

public partial class CameraNPC : NPC
{
    private Camera2D _camera = new Camera2D();

    [Export] public Vector2 Offset { get; set; }
    [Export] public Vector2 DefaultZoom { get; set; }

    public CameraNPC()
    {
        AddChild(_camera);
    }

    public override void _Ready()
    {
        base._Ready();
        _camera.Zoom = DefaultZoom;
        _camera.Offset = Offset;
    }

    public void ChangeZoom(float zoom, float duration)
    {
        CreateTween().TweenProperty(_camera, "zoom", new Vector2(zoom, zoom), duration).SetTrans(Tween.TransitionType.Sine);
    }

    public void ChangeEnabled (bool enabled) =>
        _camera.Enabled = enabled;
}
