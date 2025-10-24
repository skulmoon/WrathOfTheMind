using Godot;
using Newtonsoft.Json.Linq;
using System;

public abstract partial class AbilityBar : ProgressBar
{
    private Label _label;
    private string _text;
    private Tween _currentTween;

    public AbilityBar()
    {
        Global.SceneObjects.PlayerChanged += OnPlayerChanged;
    }

    public override void _Ready()
    {
        _label = GetNode<Label>("Label");
        if (_text != null)
            _label.Text = _text;
    }

    public abstract void OnPlayerChanged(Player player);

    public void OnAbilityReloadStarted(float time)
    {
        Value = 0;
        MaxValue = time;
        _currentTween = CreateTween();
        _currentTween.TweenProperty(this, "value", time, time);
    }

    public void SetAbilityName(string name)
    {
        if (_label == null)
            _text = name;
        else
            _label.Text = name;
    }

    public void Close()
    {
        SetAbilityName(string.Empty);
        _currentTween?.Stop();
        Value = 0;
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.PlayerChanged -= OnPlayerChanged;
    }
}
