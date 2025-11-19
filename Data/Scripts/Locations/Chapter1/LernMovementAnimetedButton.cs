using Godot;
using System;
using System.Globalization;
using System.Collections.Generic;

public partial class LernMovementAnimetedButton : Sprite2D
{
    private RichTextLabel _text;
    private List<Vector2> _startCharPositions = new List<Vector2>();
    private Location _location;

    [Export] public Label[] Labels { get; set; }

    public override void _Ready()
    {
        _text = GetNode<RichTextLabel>("Text");
        Global.SceneObjects.LocationChanged += OnLocationChanged;
    }

    public void OnLocationChanged(Location location)
    {
        _location = location;
        if ((bool?)location.LocationData?.Find(x => x.ID == 1).Value ?? false)
        {
            Visible = false;
            return;
        }
        location.LocationData.Add((1, true));
        Global.JSON.SetLocationData(location.LocationData);
        _text.Text = string.Format(_text.Text, GetActionKey("left"), GetActionKey("up"), GetActionKey("down"), GetActionKey("right"), GetActionKey("acceleration"));
        foreach (var label in Labels)
            _startCharPositions.Add((Vector2)(((ShaderMaterial)label.Material)?.GetShaderParameter("mask_offset")));
        SetMaskPosition(new Vector2(0.15f, 0));
        SetTextMaskPosition(new Vector2(0.15f, 0));
        SetCharMaskPosition(new Vector2(1f, 0));
        Tween tween = CreateTween();
        tween.SetTrans(Tween.TransitionType.Sine);
        tween.TweenMethod(new Callable(this, nameof(SetMaskPosition)), new Vector2(0.15f, 0), new Vector2(-2.5f, 0), 3.5f);
        tween.SetParallel();
        tween.TweenMethod(new Callable(this, nameof(SetTextMaskPosition)), new Vector2(0.15f, 0), new Vector2(-2.5f, 0), 3.5f);
        tween.SetParallel();
        tween.TweenProperty(this, "position", Position + new Vector2(-230, 0), 1.5f);
        tween.TweenProperty(this, "position", Position + new Vector2(-230, 0), 1.5f);
        tween.SetParallel();
        tween.TweenMethod(new Callable(this, nameof(SetCharMaskPosition)), new Vector2(0, 0), new Vector2(-1.5f, 0), 3);
        tween.Chain();
        tween.TweenCallback(new Callable(this, "StartSimpleAnimation"));
    }

    private string GetActionKey(string action)
    {
        string strKey = string.Empty;
        foreach (var key in InputMap.ActionGetEvents(action))
            if (key is InputEventKey eventKey)
                strKey = eventKey.Keycode.ToString();
            else if (key is InputEventMouseButton eventMouse)
                strKey = eventMouse.ButtonIndex.GetName();
        return strKey;
    }

    public void SetMaskPosition(Vector2 position) =>
        ((ShaderMaterial)Material)?.SetShaderParameter("mask_offset", position);

    public void SetTextMaskPosition(Vector2 position) =>
        ((ShaderMaterial)_text.Material)?.SetShaderParameter("mask_offset", position);

    public void SetCharMaskPosition(Vector2 position)
    {
        for (int i = 0; i < Labels.Length; i++)
            ((ShaderMaterial)Labels[i].Material)?.SetShaderParameter("mask_offset", _startCharPositions[i] + position);
    }

    public void StartSimpleAnimation()
    {
        Tween tween2 = CreateTween();
        tween2.SetTrans(Tween.TransitionType.Sine);
        tween2.SetLoops();
        tween2.TweenProperty(this, "position", Position + new Vector2(0, 10), 5);
        tween2.Chain();
        tween2.TweenProperty(this, "position", Position + new Vector2(0, -10), 5);
    }

    public new void Hide()
    {
        if ((bool?)_location?.LocationData?.Find(x => x.ID == 3).Value ?? false)
        {
            return;
        }
        _location.LocationData.Add((3, true));
        Global.JSON.SetLocationData(_location.LocationData);
        Material = null;
        _text.Material = null;
        foreach (var label in Labels)
            label.Material = null;
        Tween tween = CreateTween();
        tween.TweenProperty(this, "modulate:a", 0, 1f);
        tween.TweenCallback(new Callable(this, "SetVisible"));
    }

    public void SetVisible() =>
        Visible = false;

    public override void _ExitTree()
    {
        Global.SceneObjects.LocationChanged -= OnLocationChanged;
    }
}
