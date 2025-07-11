using Godot;
using System;

public partial class ExplosionSkeleton : EnemyReload
{
    private IExplosionSkeletonState _state;

    public CustomTrigger Trigger { get; set; }
    public IExplosionSkeletonState State
    {
        get => _state;
        set
        {
            SetState((Node)value, (Node)_state);
            _state = value;
        }
    }

    public ExplosionSkeleton() : base(reloadTime: 1, speed: 140, damage: 80, health: 50, "Charapter1/MeleSkeleton.tscn")
    {
        State = new CalmExplosionSkeletonState(this);
        Trigger = new CustomTrigger(4,
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
        AddChild(Trigger);
    }
}
