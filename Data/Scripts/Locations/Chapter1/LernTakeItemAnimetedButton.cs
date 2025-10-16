using Godot;
using System;

public partial class LernTakeItemAnimetedButton : AnimatedSprite2D
{
    private RichTextLabel _text;
    private Label _char;
    private bool _isActive = false;

    [Export] public Texture2D Mask { get; set; }

    public override void _Ready()
    {
        _text = GetNode<RichTextLabel>("Text");
        _char = GetNode<Label>("Char");
        ((ShaderMaterial)Material)?.SetShaderParameter("mask_offset", Vector2.Zero);
        ((ShaderMaterial)_text.Material)?.SetShaderParameter("mask_offset", new Vector2(-0.3f, 0));
        ((ShaderMaterial)_char.Material)?.SetShaderParameter("mask_offset", new Vector2(-0.115f, 0));
    }

    public void Activate()
	{
        if ((bool?)Global.SceneObjects.Location.LocationData.Find(x => x.ID == 2).Value ?? false)
        {
            return;
        }
        Global.SceneObjects.Location.LocationData.Add((2, true));
        Global.JSON.SetLocationData(Global.SceneObjects.Location.LocationData);
        string str = Global.Settings.SaveData.CurrentLocation;
        Play();
        Tween tween1 = CreateTween();
        tween1.SetTrans(Tween.TransitionType.Sine);
        tween1.TweenMethod(new Callable(this, nameof(SetMaskPosition)), new Vector2(0, 0), new Vector2(0, -1), 1.5f);
        tween1.SetParallel();
        tween1.TweenMethod(new Callable(this, nameof(SetCharMaskPosition)), new Vector2(0, 0), new Vector2(0, -1), 1.5f);
        tween1.SetParallel();
        tween1.TweenProperty(this, "position", Position + new Vector2(0, -80), 1.5f);
        tween1.Chain();
        tween1.TweenMethod(new Callable(this, nameof(SetTextMaskPosition)), new Vector2(-0.3f, 0), new Vector2(1, 0), 1.5f);
        tween1.SetParallel();
        tween1.TweenProperty(_text, "position", _text.Position + new Vector2(136, 0), 1.5f);
        string strKey = string.Empty;
        foreach (var key in InputMap.ActionGetEvents("interact"))
            if (key is InputEventKey eventKey)
                strKey = eventKey.Keycode.ToString();
            else if (key is InputEventMouseButton eventMouse)
                strKey = eventMouse.ButtonIndex.GetName();
        _text.Text += $"\"{strKey}\".";
        _char.Text = strKey;
        GetNode<DroppedItem>("%StartShard").Take += () =>
        {
            Material = null;
            _text.Material = null;
            CreateTween().TweenProperty(this, "modulate:a", 0, 0.5f);
        };
        Tween tween2 = CreateTween();
        tween2.SetLoops();
        tween2.TweenProperty(_char, "position", _char.Position, 9f / 12f);
        tween2.Chain();
        tween2.TweenProperty(_char, "position", _char.Position + new Vector2(0, 4), 0);
        tween2.Chain();
        tween2.TweenProperty(_char, "position", _char.Position + new Vector2(0, 4), 11f / 12f);
        tween2.Chain();
        tween2.TweenProperty(_char, "position", _char.Position, 0);
    }

    public void SetMaskPosition(Vector2 position) =>
        ((ShaderMaterial)Material)?.SetShaderParameter("mask_offset", position);

    public void SetTextMaskPosition(Vector2 position) =>
        ((ShaderMaterial)_text.Material)?.SetShaderParameter("mask_offset", position);

    public void SetCharMaskPosition(Vector2 position) =>
        ((ShaderMaterial)_char.Material)?.SetShaderParameter("mask_offset", position);
}
