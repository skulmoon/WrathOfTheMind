using Godot;
using System;

public partial class PowerProgressBar : ProgressBar
{
    public override void _Ready()
    {
        Global.SceneObjects.PlayerChanged += PlayerChanged;
    }

    public void PlayerChanged(Player player)
    {
        player.ChangedPower += OnChangedPower;
        Value = player.Stamina;
        MaxValue = player.MaxStamina;
    }

    public void OnChangedPower(float power)
    {
        Value = power;
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.PlayerChanged -= PlayerChanged;
    }

    public void HideBar()
    {
        CreateTween().TweenProperty(this, "position:x", Position.X - 80, 0.5f);
        CreateTween().TweenProperty(this, "modulate:a", 0, 0.5f);
    }

    public void ShowBar()
    {
        CreateTween().TweenProperty(this, "position:x", Position.X + 80, 0.5f);
        CreateTween().TweenProperty(this, "modulate:a", 1, 0.5f);
    }
}
