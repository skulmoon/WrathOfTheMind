using Godot;
using System;
using System.Collections.Generic;

public abstract partial class ArmorAbility : Armor2D
{
    private Timer _timer1;
    private Timer _timer2;

    public event Action<ArmorAbility, float, List<PlayerAttack>> FirstAbilityUssed;
    public event Action<ArmorAbility, float, List<PlayerAttack>> SecondAbilityUsseded;

    public ArmorAbility(float protection, int additionalHealth) : base(protection, additionalHealth)
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
        if (Input.IsActionPressed("armor_ability1") && _timer1.TimeLeft == 0)
        {
            FirstAbilityUssed?.Invoke(this, (float)_timer1.WaitTime, Ability1());
            if (_timer1.IsInsideTree())
                _timer1.Start();
        }
        if (Input.IsActionPressed("armor_ability2") && _timer2.TimeLeft == 0)
        {
            SecondAbilityUsseded?.Invoke(this, (float)_timer2.WaitTime, Ability2());
            if (_timer2.IsInsideTree())
                _timer2.Start();
        }
    }

    public abstract string[] GetAbilityNames();
    public abstract List<PlayerAttack> Ability1();
    public abstract List<PlayerAttack> Ability2();
}
