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
        MaxValue = player.MaxPower;
    }

    public void OnChangedPower(float health)
    {
        Value = health;
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.PlayerChanged -= PlayerChanged;
    }
}
