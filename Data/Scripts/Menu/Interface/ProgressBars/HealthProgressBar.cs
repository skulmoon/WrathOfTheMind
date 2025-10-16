using Godot;
using System;

public partial class HealthProgressBar : ProgressBar
{
    public override void _Ready()
    {
        Global.SceneObjects.PlayerChanged += PlayerChanged;
    }

    public void PlayerChanged(Player player)
    {
        player.HitBox.ChangeHealth += ChangeHealth;
        MaxValue = player.HitBox.MaxHealth;
        Value = player.HitBox.Health;
    }

    public void ChangeHealth(int health)
    {
        Value = health;
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.PlayerChanged -= PlayerChanged;
    }
}
