using Godot;
using System;
using System.Collections.Generic;
using System.Security;

public partial class Prologue : Location
{
    private Timer _timer;

    public override void _Ready()
    {
        base._Ready();
        CutSceneCustomizes.Add((1, () => Global.SceneObjects.Npcs.Find((x) => x.ID == 2).Velocity = Vector2.Zero));
        CutSceneCustomizes.Add((2, () =>
        {
            Global.SceneObjects.Npcs.Find((x) => x.ID == 2).Velocity = Vector2.Zero;
            Global.SceneObjects.Player.Visible = true;
            CreateTween().TweenProperty(Global.SceneObjects.Npcs.Find((x) => x.ID == 2), "Speed", 300, 5);
        }));
        CutSceneCustomizes.Add((3, () => {
            Input.ParseInputEvent(new InputEventAction() { Action = "interact", Pressed = true });
            Global.SceneObjects.Player.GetNode<Camera2D>("Camera").Enabled = true;
            ((CameraNPC)Global.SceneObjects.Npcs.Find((x) => x.ID == 3)).Camera.Enabled = false;
        }
        ));
        CutSceneCustomizes.Add((4, () => {
            Global.SceneObjects.Player.GetNode<Camera2D>("Camera").Enabled = false;
            CreateTween().TweenProperty(Global.SceneObjects.Npcs.Find((x) => x.ID == 2), "Speed", 20, 4.5);
            CreateTween().TweenProperty(Global.SceneObjects.Npcs.Find((x) => x.ID == 3), "Speed", 20, 4.5);
            CreateTween().TweenProperty(GetTree().CurrentScene.GetNode<TextureRect>("%Dark"), "modulate:a", 0, 3);
        }));
        CutSceneCustomizes.Add((5, () => Global.SceneObjects.Npcs.Find((x) => x.ID == 3).Velocity = Vector2.Zero));
        CutSceneCustomizes.Add((6, () => { 
            ((CameraNPC)Global.SceneObjects.Npcs.Find((x) => x.ID == 3)).ChangeZoom(1, 3);
            Global.SceneObjects.Npcs.Find((x) => x.ID == 3).Velocity = Vector2.Zero;
        }));
        CutSceneCustomizes.Add((7, () => {

            foreach (Tween tween in GetTree().GetProcessedTweens())
                if (tween.IsValid())
                    tween.Kill();
            ((CameraNPC)Global.SceneObjects.Npcs.Find((x) => x.ID == 3)).Camera.Enabled = false;
            Global.SceneObjects.Player.GetNode<Camera2D>("Camera").Enabled = true;
            Global.SceneObjects.Player.Visible = true;
            GetTree().CurrentScene.GetNode<TextureRect>("%Dark").Visible = false;
            Input.ParseInputEvent(new InputEventAction { Action = "interact" });
        }
        ));
        if ((bool?)LocationData.Find(x => x.ID == 1).Value ?? true)
        {
            Global.SceneObjects.Player.Visible = false;
            Global.SceneObjects.Player.GetNode<Camera2D>("Camera").Enabled = false;
            GetTree().CurrentScene.GetNode<TextureRect>("%Dark").Visible = true;
            LocationData.Add((1, false));
            Global.JSON.SetLocationData(LocationData);
            Global.CutSceneManager.OutputCutScene(2, 1);
        }
    }
}
