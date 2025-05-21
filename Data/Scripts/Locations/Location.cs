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
        LocationData = Global.JSON.GetLocationData() ?? new List<(int ID, object Value)>();
        Global.SceneObjects.Location = this;
    }
}
