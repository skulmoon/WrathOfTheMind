using Godot;
using System;

public abstract partial class ArmorAbility : Armor2D
{
    private Timer _timer1;
    private Timer _timer2;

    public ArmorAbility(float protection, float additionalHealth) : base(protection, additionalHealth)
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
        if (Input.IsActionJustPressed("armor_ability1") && _timer1.TimeLeft == 0)
        {
            Ability1();
            _timer1.Start();
        }
        if (Input.IsActionJustPressed("armor_ability2") && _timer2.TimeLeft == 0)
        {
            Ability2();
            _timer2.Start();
        }
    }

    public abstract void Ability1();
    public abstract void Ability2();
}
