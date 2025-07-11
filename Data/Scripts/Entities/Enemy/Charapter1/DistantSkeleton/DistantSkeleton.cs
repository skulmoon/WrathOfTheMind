using Godot;
using System;

public partial class DistantSkeleton : Enemy
{
    private IDistantSkeletonState _state;
    private Player _player;

    public CustomTrigger Trigger { get; set; }
    public IDistantSkeletonState State
    {
        get => _state;
        set
        {
            GD.Print(value.GetAnimation());
            SetState((Node)value, (Node)_state);
            _state = value;
        }
    }

    public DistantSkeleton() : base(speed: 200, damage: 20, health: 100, "Charapter1/DistantSkeleton.tscn")
    {
        State = new СalmDistantSkeletonState(this);
        Trigger = new CustomTrigger(4,
            new CircleShape2D()
            {
                Radius = 400,
            },
            (node) =>
            {
                if (node is Player)
                    State.Attack();
            }
        );
        AddChild(Trigger);
    }

    public override void TakeDamage(float damage)
    {
        GD.Print($"Enemy take {damage} damage.");
        base.TakeDamage(damage);
    }
}
