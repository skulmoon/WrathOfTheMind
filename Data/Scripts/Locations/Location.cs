using Godot;
using System;
using System.Collections.Generic;

public partial class Location : Node2D
{
    public List<(int Number, Action Action)> CutSceneCustomizes { get; set; } = new List<(int Number, Action Action)>();
    public List<(int ID, object Value)> LocationData { get; private set; }

    public override void _Ready()
    {
        YSortEnabled = true;
        LocationData = Global.JSON.GetLocationData();
        if (LocationData == null)
        {
            LocationData = new List<(int ID, object Value)>();
            Global.SaveManager.CreateLocationData();
        }
        Global.Variables.CutScene = false;
        Global.SceneObjects.Location = this;
        StarAnimation();
    }

    public virtual void StarAnimation()
    {
        UIDark dark = GetNode<Interface>("%Interface").MenuDark;
        dark.CurrentDarkPower = 1;
        dark.Visible = true;
        Tween tween = CreateTween();
        tween.TweenProperty(dark, "CurrentDarkPower", 1, 0.2);
        tween.Chain();
        tween.TweenProperty(dark, "CurrentDarkPower", -0.2, 0.5);
    }

    public Action GetCutSceneCustomize(int id) =>
        CutSceneCustomizes.Find(x => x.Number == id).Action;

    public void SetData(int id, object value, bool isForSave = false)
    {
        int index = LocationData.FindIndex(x => x.ID == id);
        if (index != -1)
            LocationData[index] = (id, value);
        else
            LocationData.Add((id, value));
        if (isForSave)
            Global.JSON.SetLocationData(LocationData);
    }

    public T GetData<T>(int id) =>
        (T)LocationData.Find(x => x.ID == id).Value;
}
