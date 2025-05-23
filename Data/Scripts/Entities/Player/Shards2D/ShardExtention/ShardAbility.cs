using Godot;
using System;

public abstract partial class ShardAbility : Shard2D
{
    private Timer _timer1;
    private Timer _timer2;

    public bool IsMain { get; set; }

    public ShardAbility(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, float maxRange) : base(zeroHealth, health, damage, speed, timeReload, critChance, maxRange)
    {
        _timer1 = new Timer()
        {
            WaitTime = 1,
            Autostart = true,
            OneShot = true,
        };
        AddChild(_timer1);
        _timer2 = new Timer()
        {
            WaitTime = 1,
            Autostart = true,
            OneShot = true,
        };
        AddChild(_timer2);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("shard_ability1") && IsMain && _timer1.TimeLeft == 0)
        {
            Ability1();
            _timer1.Start();
        }
        if (Input.IsActionJustPressed("shard_ability2") && IsMain && _timer2.TimeLeft == 0)
        {
            Ability2();
            _timer2.Start();
        }
    }

    public abstract void Ability1();
    public abstract void Ability2();
}
