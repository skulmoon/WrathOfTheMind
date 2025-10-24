using Godot;
using System;

public abstract partial class DisappearPlayerAttack : PlayerAttack
{
    private Timer _lifeTime;

    public DisappearPlayerAttack(int health, int damage, float critChance, float lifeTime, bool defaultCollision = true) : base(health, damage, critChance, defaultCollision)
    {
        _lifeTime = new Timer()
        {
            WaitTime = lifeTime,
            Autostart = true,
            OneShot = true,

        };
        AddChild(_lifeTime);
        _lifeTime.Timeout += Destroy;
    }
}
