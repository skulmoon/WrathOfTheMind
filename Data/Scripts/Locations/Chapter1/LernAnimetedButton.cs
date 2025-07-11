using Godot;
using System;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

public partial class LernAnimetedButton : AnimatedSprite2D
{
    private RichTextLabel _text;
    private Label _char;

    [Export] public Texture2D Mask { get; set; }

    public override void _Ready()
	{
        Play();
        _text = GetNode<RichTextLabel>("Text");
        _char = GetNode<Label>("Char");
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
        GetNode<DroppedItem>("%StartShard").Take += () =>
        {
            Material = null;
            _text.Material = null;
            CreateTween().TweenProperty(this, "modulate:a", 0, 0.5f);
        };
        _char.Text = strKey;
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
