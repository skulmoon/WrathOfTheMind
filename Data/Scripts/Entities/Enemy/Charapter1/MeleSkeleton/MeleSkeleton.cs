using Godot;
using System;

public partial class MeleSkeleton : EnemyReload
{
    private IMeleSkeletonState _state;

    public CustomTrigger MeleTrigger { get; set; }
    public CustomTrigger ThrowTrigger { get; set; }

    public IMeleSkeletonState State
    {
        get => _state;
        set
        {
            SetState((Node)value, (Node)_state);
            _state = value;
        }
    }

    public MeleSkeleton() : base(reloadTime: 1, speed: 80, damage: 30, health: 50, "Charapter1/MeleSkeleton.tscn")
    {
        State = new CalmMeleSkeletonState(this);
        MeleTrigger = new CustomTrigger(4,
            new CircleShape2D()
            {
                Radius = 300,
            },
            (node) =>
            {
                if (node is Player)
                    State.Attack();
            },
            (node) =>
            {
                if (node is Player)
                    State.Chase();
            }
        );
        AddChild(MeleTrigger);
        ThrowTrigger = new CustomTrigger(4,
            new CircleShape2D()
            {
                Radius = 250,
            },
            bodyExited: (node) =>
            {
                if (node is Player)
                    State.ThrowShovel();
            }
        );
        AddChild(ThrowTrigger);
    }
}
