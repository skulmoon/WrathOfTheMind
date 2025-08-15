using Godot;
using System;

public partial class ButtonMenuTransition : CustomButton
{
    [Export] public Control LastMenu { get; set; }
    [Export] public Control NextMenu { get; set; }

    public ButtonMenuTransition()
    {
        Pressed += OnPressed;
    }

    public void OnPressed()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(LastMenu, "modulate:a", 0, 0.5f);
        tween.TweenCallback(new Callable(this, "ChangeLastMenuVisible"));
    }

    public void ChangeLastMenuVisible()
    {
        LastMenu.Visible = false;
        NextMenu.Visible = true;
        NextMenu.Modulate = new Color(1, 1, 1, 0);
        Tween tween = CreateTween();
        tween.TweenProperty(NextMenu, "modulate:a", 1, 0.5f);
    }
}
