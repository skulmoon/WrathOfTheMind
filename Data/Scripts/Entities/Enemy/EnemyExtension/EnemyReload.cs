using Godot;
using System;

public abstract partial class EnemyReload : Enemy
{
    private Timer _reloadTimer;

    public EnemyReload(float reloadTime, float speed, int damage, int health, string animation) : base(speed, damage, health, animation)
	{
        _reloadTimer = new Timer()
        {
            WaitTime = reloadTime,
            Autostart = true,
            OneShot = true,
        };
        AddChild(_reloadTimer);
    }

    public override void Attack(EnemyAttack attack)
    {
        if (IsLoad())
        {
            GetTree().CurrentScene.AddChild(attack);
            Reload();
        }
    }

    public void Reload() =>
        _reloadTimer.Start();

    public bool IsLoad() =>
        _reloadTimer.TimeLeft == 0;
}
