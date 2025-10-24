using Godot;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;

public partial class ConductivePath : Area2D, IInteractionArea
{
	private bool _playerEntered = false;
    private Location _currentLocation;

    public bool IsActive { get; set; } = false;
    [Export] public string Path { get; set; }
    [Export] public Vector2 EndPosition { get; set; }
    [Export] public string Animation { get; set; }

    public ConductivePath()
    {
        CollisionLayer = 8;
        Global.SceneObjects.LocationChanged += OnLocationChanged;
    }

    public void Interaction()
    {
        var tree = GetTree();
        Global.Settings.SaveData.CurrentLocation = Path;
        tree.CurrentScene.GetNode<TextureRect>("%Dark").Visible = true;
        tree.CurrentScene.GetNode<TextureRect>("%Dark").Modulate = new Color(0, 0, 0, 0);
        Tween firstTween = CreateTween();
        firstTween.TweenProperty(GetTree().CurrentScene.GetNode<TextureRect>("%Dark"), "modulate:a", 1, 0.5f);
        IsActive = true;
        firstTween.TweenCallback(Callable.From(() => 
        {
            Global.SceneObjects.ChangeScene($"res://Data/Scenes/Location/{Path}.tscn");
        }));
    }

    public void OnLocationChanged(Location location)
    {
        if (_currentLocation != null && IsActive)
        {
            GD.Print(EndPosition);
            Global.SceneObjects.Player.GlobalPosition = EndPosition;
            _currentLocation = null;
            Global.SceneObjects.LocationChanged -= OnLocationChanged;
        }
        else
            _currentLocation = location;
    }
}
