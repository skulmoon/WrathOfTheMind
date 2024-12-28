using Godot;
using System;

public abstract partial class EnemyReload : Enemy
{
    private Timer _reloadTimer;

    public EnemyReload() : base()
    {
        _reloadTimer = new Timer()
        {
            WaitTime = 3,
            Autostart = true
        };
        AddChild(_reloadTimer);
        _reloadTimer.Timeout += Attack;
    }

    public EnemyReload(float reloadTime, float speed, int damage, int health) : base(speed, damage, health)
	{
        _reloadTimer = new Timer()
        {
            WaitTime = reloadTime,
            Autostart = true
        };
        AddChild(_reloadTimer);
        _reloadTimer.Timeout += Attack;
    }

    public void Reload() =>
        _reloadTimer.Start();
}
