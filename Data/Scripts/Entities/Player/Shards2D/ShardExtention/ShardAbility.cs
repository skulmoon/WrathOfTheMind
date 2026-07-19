using Godot;
using System;
using System.Collections.Generic;

public abstract partial class ShardAbility : Shard2D
{
    private Timer _timer1;
    private Timer _timer2;
    private Timer _delayTimer;

    public override bool IsMain 
    { 
        get => base.IsMain; 
        set
        {
            StartAbilityDelay();
            base.IsMain = value;
        }
    }

    public event Action<ShardAbility, float, List<PlayerAttack>> FirstAbilityUssed;
    public event Action<ShardAbility, float, List<PlayerAttack>> SecondAbilityUssed;

    public ShardAbility(Action<Shard2D> zeroHealth, Shard shard, int number) : base(zeroHealth, shard, number)
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
        _delayTimer = new Timer()
        {
            WaitTime = 1,
            Autostart = true,
            OneShot = true,
        };
        AddChild(_delayTimer);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("shard_ability1") && IsMain && _timer1.TimeLeft == 0 && _delayTimer.TimeLeft == 0)
        {
            FirstAbilityUssed?.Invoke(this, (float)_timer1.WaitTime, Ability1());
            if (_timer1.IsInsideTree())
                _timer1.Start();
        }
        if (Input.IsActionPressed("shard_ability2") && IsMain && _timer2.TimeLeft == 0 && _delayTimer.TimeLeft == 0)
        {
            SecondAbilityUssed?.Invoke(this, (float)_timer2.WaitTime, Ability2());
            if (_timer2.IsInsideTree())
                _timer2.Start();
        }
    }

    public void StartAbilityDelay(float delay = 0.3f) =>
        _delayTimer.Start(delay);

    public abstract string[] GetAbilityNames();
    public abstract List<PlayerAttack> Ability1();
    public abstract List<PlayerAttack> Ability2();
}
