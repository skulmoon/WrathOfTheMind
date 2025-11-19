using Godot;
using System;

public partial class UIDark : TextureRect
{
    private bool _darkVisible;

    private Action DarkShowed;
    private Action DarkHided;

    [Export] public float MinlDarkPower { get; set; } = 0.1f;
    [Export] public float DurationTranslate { get; set; } = 0.1f;
    [Export] public bool DarkVisible 
    { 
        get => _darkVisible;
        set
        {
            if (_darkVisible != value)
            {
                _darkVisible = value;
                if (value)
                    ChangeDarkPower(MinlDarkPower);
                else
                    ChangeDarkPower(1);
            }
        }
    }

    public override void _Ready()
    {
        if (DarkVisible)
            ChangeDarkPower(1);
        else
            ChangeDarkPower(MinlDarkPower);
    }

    public override void _Process(double delta)
    {
        Vector2 windowHalf = GetWindow().Size / 2;
        ((ShaderMaterial)Material).SetShaderParameter("cursor_position", (-(GetGlobalMousePosition() - windowHalf) / windowHalf) * 0.05f);
    }

    public void SwitchVisible(Action VisibleEnded)
    {
        if (DarkVisible)
            ShowDark();
        else
            HideDark();
    }

    public void ShowDark(Action darkShowed)
    {
        DarkShowed += darkShowed;
        ShowDark();
    }

    public void ShowDark()
    {
        Tween tween = CreateTween();
        tween.TweenMethod(new Callable(this, "ChangeDarkPower"), MinlDarkPower, 1f, DurationTranslate);
        tween.TweenCallback(new Callable(this, "DarkShowNotify"));
    }

    public void HideDark(Action darkHided)
    {
        DarkHided += darkHided;
        HideDark();
    }

    public void HideDark()
    {
        Tween tween = CreateTween();
        tween.TweenMethod(new Callable(this, "ChangeDarkPower"), 1f, MinlDarkPower, DurationTranslate);
        tween.TweenCallback(new Callable(this, "DarkHideNotify"));
    }

    public void ChangeDarkPower(float power)
    {
        ((ShaderMaterial)Material).SetShaderParameter("dark_power", power);
    }

    public void DarkShowNotify()
    {
        DarkShowed?.Invoke();
        _darkVisible = true;
    }

    public void DarkHideNotify()
    {
        DarkHided?.Invoke();
        _darkVisible = false;
    }

    public override void _ExitTree()
    {
        DarkShowed = null;
        DarkHided = null;
    }
}
