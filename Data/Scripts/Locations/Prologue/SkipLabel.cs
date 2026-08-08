using Godot;
using System;

public partial class SkipLabel : CustomLabel
{
	public override void _Ready()
	{
		base._Ready();
		if (!Global.Variables.FistCutSceneActivated)
		{
			Visible = false;
            Global.Variables.FistCutSceneActivated = true;
			Global.JSON.SaveConfig();
        }
		else
		{
			Text = string.Format(Text, this.GetActionKey("interact"));
			Tween tween = CreateTween();
			tween.TweenProperty(this, "modulate:a", 0.6f, 0.5f).SetTrans(Tween.TransitionType.Sine);
			tween.TweenProperty(this, "modulate:a", 1f, 0.5f).SetTrans(Tween.TransitionType.Sine);
			Tween tween2 = CreateTween();
			tween2.TweenInterval(9);
			tween2.TweenProperty(this, "modulate:a", 0f, 1f).SetTrans(Tween.TransitionType.Sine);
		}
    }
}
