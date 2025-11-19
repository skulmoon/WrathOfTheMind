using Godot;
using System;

public abstract partial class ShardAbility : Shard2D
{
    private Timer _timer1;
    private Timer _timer2;

    private Action<float> _firstAbilityReloadStarted;
    private Action<float> _secondAbilityReloadStarted;

    public event Action<float> FirstAbilityReloadStarted
    {
        add
        {
            _firstAbilityReloadStarted += value;
            value.Invoke((float)_timer1.WaitTime);
        }
        remove => _firstAbilityReloadStarted -= value;
    }
    public event Action<float> SecondAbilityReloadStarted
    {
        add
        {
            _secondAbilityReloadStarted += value;
            value.Invoke((float)_timer2.WaitTime);
        }
        remove => _secondAbilityReloadStarted -= value;
    }

    public bool IsMain { get; set; } = false;

    public ShardAbility(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, int maxRange) : base(zeroHealth, health, damage, speed, timeReload, critChance, maxRange)
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
        _firstAbilityReloadStarted?.Invoke((float)_timer1.WaitTime);
        _secondAbilityReloadStarted?.Invoke((float)_timer2.WaitTime);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("shard_ability1") && IsMain && _timer1.TimeLeft == 0)
        {
            Ability1();
            if (_timer1.IsInsideTree())
            {
                _timer1.Start();
                _firstAbilityReloadStarted?.Invoke((float)_timer1.WaitTime);
            }
        }
        if (Input.IsActionPressed("shard_ability2") && IsMain && _timer2.TimeLeft == 0)
        {
            Ability2();
            if (_timer2.IsInsideTree())
            {
                _timer2.Start();
                _secondAbilityReloadStarted?.Invoke((float)_timer1.WaitTime);
            }
        }
    }

    public abstract string[] GetAbilityNames();
    public abstract void Ability1();
    public abstract void Ability2();
}
