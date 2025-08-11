using Godot;
using System;

public partial class FistMovementMeleSkeletonState : Node, IMeleSkeletonState
{
    private MeleSkeleton _enemy;

    public FistMovementMeleSkeletonState(MeleSkeleton enemy)
    {
        _enemy = enemy;
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Move(_enemy.PositionControl?.GetPosition() ?? Vector2.Zero, delta);
    }

    public void Attack()
    {
        _enemy.State = new FistAttackMeleSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "fist_movement";
}
