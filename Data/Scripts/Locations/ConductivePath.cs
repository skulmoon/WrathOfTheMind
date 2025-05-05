using Godot;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;

public partial class ConductivePath : Area2D, IInteractionArea
{
	private bool _playerEntered = false;

    [Export] public string Path { get; set; }
    [Export] public Vector2 StartPosition { get; set; }
    [Export] public Vector2 EndPosition { get; set; }
    [Export] public string Animation { get; set; }

    public void Interaction()
    {
        GD.Print(3);
        Global.Settings.GameSettings.CurrentLocation = Path;
        var tree = GetTree();
        tree.CurrentScene.GetNode<TextureRect>("%Dark").Visible = true;
        tree.CurrentScene.GetNode<TextureRect>("%Dark").Modulate = new Color(0, 0, 0, 0);
        Tween firstTween = CreateTween();
        firstTween.TweenProperty(GetTree().CurrentScene.GetNode<TextureRect>("%Dark"), "modulate:a", 1, 1);
        firstTween.TweenCallback(Callable.From(() => 
        {
            tree.ChangeSceneToFile($"res://Data/Scenes/Location/{Path}.tscn");
            GD.Print(5);
        }));
    }
}
