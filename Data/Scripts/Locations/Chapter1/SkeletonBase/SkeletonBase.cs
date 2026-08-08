using Godot;
using System;
using System.Collections.Generic;

public partial class SkeletonBase : Chapter1Location
{
    public override void _Ready()
    {
        base._Ready();
        Global.Music.PlayMusic("Skeleton.ogg");
        CutSceneCustomizes.Add((1, () =>
        {
            Tween tween = CreateTween();
            tween.TweenProperty(GetNode("%MenuDark"), "CurrentDarkPower", 1.6, 3);
            tween.TweenCallback(new Callable(this, nameof(OpenChangeLocation)));
        }
        ));
        CutSceneCustomizes.Add((2, () =>
        {
            Tween tween = CreateTween();
            tween.TweenProperty(GetNode("%MenuDark"), "CurrentDarkPower", 1.6, 3);
            ((RichTextLabel)GetNode("%Titles")).Visible = true;
            tween.TweenInterval(2);
            tween.TweenProperty(GetNode("%Titles"), "position:y", -(((Control)GetNode("%Titles")).Size.Y - GetWindow().Size.Y), 20);
            tween.TweenProperty(GetNode("%Titles"), "position:y", -(((Control)GetNode("%Titles")).Size.Y - GetWindow().Size.Y), 3);
            tween.TweenCallback(new Callable(this, nameof(OpenMenu)));
        }
        ));
        CutSceneCustomizes.Add((3, () =>
        {
            GetNode<CameraNPC>("%Camera").ChangeEnabled(true);
        }
        ));
        if (Global.CutSceneData.GetChoice(6, 1) != 2)
        {
            Global.CutSceneManager.OutputCutScene(6, 1);
        }
    }

    public void OpenChangeLocation()
    {
        Global.CutSceneManager.Disable();
        ConductivePath conductive = new ConductivePath()
        {
            Path = "Chapter1/SkeletonBaseHeadquarters",
            EndPosition = new Vector2(0, 0),
        };
        AddChild(conductive);
        conductive.Interaction();
    }

    public void OpenMenu()
    {
        Global.SaveManager.SaveGame();
        Global.Inventory.Clear();
        Global.SceneObjects.ChangeScene("res://Data/Scenes/Menu/MainMenu.tscn");
        Global.Variables.CutScene = false;
    }
}