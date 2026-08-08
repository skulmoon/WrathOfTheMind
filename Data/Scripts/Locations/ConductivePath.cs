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
        Global.SaveManager.SaveGame();
        var tree = GetTree();
        Global.Variables.SaveData.CurrentLocation = Path;
        Tween firstTween = CreateTween();
        firstTween.TweenProperty(GetTree().CurrentScene.GetNode<Interface>("%Interface").MenuDark, "CurrentDarkPower", 1.6f, 0.5f);
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
            if (Global.SceneObjects.Player != null)
            {
                Global.SceneObjects.Player.GlobalPosition = EndPosition;
                Global.SceneObjects.Player.Sprite.Play(Animation);
            }
            _currentLocation = null;
            Global.SceneObjects.LocationChanged -= OnLocationChanged;
        }
        else
            _currentLocation = location;
    }
}
