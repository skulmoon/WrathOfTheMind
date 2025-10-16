using Godot;
using System;
using System.Collections.Generic;

public partial class Location : Node2D
{
    public List<(int Number, Action Action)> CutSceneCustomizes { get; set; } = new List<(int Number, Action Action)>();
    public List<(int ID, object Value)> LocationData { get; private set; }

    public Action GetCutSceneCustomize(int id) =>
        CutSceneCustomizes.Find(x => x.Number == id).Action;

    public override void _Ready()
    {
        LocationData = Global.JSON.GetLocationData();
        if (LocationData == null)
        {
            LocationData = new List<(int ID, object Value)>();
            Global.SaveManager.CreateLocationData();
        }
        Global.SceneObjects.Location = this;
        TextureRect dark = GetNode<TextureRect>("%Dark");
        dark.Visible = true;
        Tween tween = CreateTween();
        tween.TweenProperty(dark, "modulate:a", 1, 0.2);
        tween.Chain();
        tween.TweenProperty(dark, "modulate:a", 0, 0.5);
    }
}
